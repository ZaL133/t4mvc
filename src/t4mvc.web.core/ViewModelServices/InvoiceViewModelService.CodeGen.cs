using AutoMapper;
using t4mvc.core;
using t4mvc.data.Services;
using t4mvc.web.core;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace t4mvc.web.core.ViewModelServices
{
    public partial interface IInvoiceViewModelService
    {
	    IQueryable<InvoiceViewModel> GetAllInvoices();
        InvoiceViewModel Find(Guid invoiceId);
        void CreateInvoice(InvoiceViewModel invoiceViewModel);
        void SaveInvoice(InvoiceViewModel invoiceViewModel);
		void DeleteInvoice(InvoiceViewModel invoiceViewModel);
		void Hydrate(InvoiceViewModel invoiceViewModel);


    }
    public partial class InvoiceViewModelService : IInvoiceViewModelService
    {

        private readonly IInvoiceService invoiceService;
        private readonly IProjectService projectService;
        private readonly IContextHelper contextHelper;
        private readonly IUserService userService;

        private readonly IAuditService auditService;
        

        public IQueryable<InvoiceViewModel> GetAllInvoices()
        {
		    var query = (from invoice in invoiceService.GetAllInvoices()
                         
                         join project_ProjectId in projectService.GetAllProjects() on invoice.ProjectId equals project_ProjectId.ProjectId
                         
			             select new InvoiceViewModel
						 {
							InvoiceId = invoice.InvoiceId,						 
							ProjectId = invoice.ProjectId,						 
							InvoiceName = invoice.InvoiceName,						 
							InvoiceDate = invoice.InvoiceDate,						 
							InvoiceAmount = invoice.InvoiceAmount,						 
							Status = invoice.Status,						 
                            ProjectIdProjectName = project_ProjectId.ProjectName, });
            return query;
        }

        public InvoiceViewModelService(IInvoiceService invoiceService,IProjectService projectService, IUserService userService,
                                            IContextHelper contextHelper, IAuditService auditService )
        {
            this.invoiceService = invoiceService;
            this.contextHelper      = contextHelper;

            this.projectService = projectService;

            this.auditService = auditService;

        }

        public void CreateInvoice(InvoiceViewModel invoiceViewModel)
        {
            var invoice = invoiceViewModel.Map<Invoice>();
            invoice.CreateDate = DateTime.Now;
            invoice.ModifyDate = DateTime.Now;
            invoice.CreateUserId = Current.UserId;
            invoice.ModifyUserId = Current.UserId;
            invoice.InvoiceId = Guid.NewGuid();

            invoiceService.CreateInvoice(invoice);

            contextHelper.SaveChanges();

            // Set the new id back on the view so it can be called post-save
            invoiceViewModel.InvoiceId = invoice.InvoiceId;
        }

        public InvoiceViewModel Find(Guid invoiceId)
        {
            var invoice = invoiceService.Find(invoiceId)
                                                  .Map<InvoiceViewModel>();


            if (invoice.ProjectId != null)
                invoice.ProjectIdProjectName = projectService.Find(invoice.ProjectId)?.ProjectName;
            Hydrate(invoice);
            return invoice;
        }

        public void SaveInvoice(InvoiceViewModel invoiceViewModel)
        {
            var invoice = invoiceViewModel.Map<Invoice>();
            invoice.ModifyDate = DateTime.Now;
            invoice.ModifyUserId = Current.UserId;

            var ignore = FieldMappingInspector<Invoice>.GetAllReadonlyFields(invoiceViewModel);

            invoiceService.UpdateInvoice(invoice, ignore);

            contextHelper.SaveChanges();
        }

		public void DeleteInvoice(InvoiceViewModel invoiceViewModel)
        {
            var invoice = invoiceViewModel.Map<Invoice>();

            invoiceService.DeleteInvoice(invoice);

            contextHelper.SaveChanges();
        }

        public string GetProjectIdProjectName(Guid? id)
        {
			return projectService.Find(id)?.ProjectName;
        }

        public void Hydrate(InvoiceViewModel invoiceViewModel)
        {
            var id = invoiceViewModel.InvoiceId;

            invoiceViewModel.AuditHistory = GetAuditRecords(id);

            invoiceViewModel.ProjectIdProjectName =     GetProjectIdProjectName(invoiceViewModel.ProjectId);
        
        }


        public List<AuditRecord> GetAuditRecords(Guid invoiceId)
        {
            return auditService.GetAuditRecords()
                               .Where(x => x.RecordId == invoiceId && x.RecordType == "Invoice")
                               .ToList();
        }


    }
}
