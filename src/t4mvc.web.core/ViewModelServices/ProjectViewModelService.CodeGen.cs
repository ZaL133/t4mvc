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
    public partial interface IProjectViewModelService
    {
	    IQueryable<ProjectViewModel> GetAllProjects();
        ProjectViewModel Find(Guid projectId);
        void CreateProject(ProjectViewModel projectViewModel);
        void SaveProject(ProjectViewModel projectViewModel);
		void DeleteProject(ProjectViewModel projectViewModel);
		void Hydrate(ProjectViewModel projectViewModel);
        List<ProjectLogViewModel> GetProjectLogs(Guid projectId);
        List<InvoiceViewModel> GetInvoices(Guid projectId);
        List<NoteViewModel> GetNotes(Guid projectId);

    }
    public partial class ProjectViewModelService : IProjectViewModelService
    {
        private readonly IProjectService projectService;
        private readonly IAccountService accountService;
        private readonly IContactService contactService;
        private readonly IProjectLogViewModelService projectLogViewModelService;
        private readonly IInvoiceViewModelService invoiceViewModelService;
        private readonly INoteViewModelService noteViewModelService;
        private readonly IContextHelper contextHelper;
        private readonly IUserService userService;
        private readonly IAuditService auditService;
        
        public IQueryable<ProjectViewModel> GetAllProjects()
        {
		    var query = (from project in projectService.GetAllProjects()
                         						 join account_AccountId in accountService.GetAllAccounts() on project.AccountId equals account_AccountId.AccountId into left_tmp_account_AccountId
						 from left_account_AccountId in left_tmp_account_AccountId.DefaultIfEmpty()
						 						 join contact_PrimaryContactId in contactService.GetAllContacts() on project.PrimaryContactId equals contact_PrimaryContactId.ContactId into left_tmp_contact_PrimaryContactId
						 from left_contact_PrimaryContactId in left_tmp_contact_PrimaryContactId.DefaultIfEmpty()
						 			             select new ProjectViewModel
						 {							ProjectId = project.ProjectId,
						 							ProjectName = project.ProjectName,
						 							StartDate = project.StartDate,
						 							DueDate = project.DueDate,
						 							AccountId = project.AccountId,
						 							PrimaryContactId = project.PrimaryContactId,
						 							Description = project.Description,
						 							EstimatedIncome = project.EstimatedIncome,
						  AccountIdName = left_account_AccountId.Name,  PrimaryContactIdEmailAddress = left_contact_PrimaryContactId.EmailAddress, });
            return query;
        }

        public ProjectViewModelService(IProjectService projectService,IAccountService accountService,IContactService contactService,IProjectLogViewModelService projectLogViewModelService,IInvoiceViewModelService invoiceViewModelService,INoteViewModelService noteViewModelService, IUserService userService,
                                            IContextHelper contextHelper, IAuditService auditService )
        {
            this.projectService = projectService;
            this.contextHelper      = contextHelper;
            this.accountService = accountService;
            this.contactService = contactService;
            this.projectLogViewModelService = projectLogViewModelService;
            this.invoiceViewModelService = invoiceViewModelService;
            this.noteViewModelService = noteViewModelService;
            this.auditService = auditService;

        }

        public void CreateProject(ProjectViewModel projectViewModel)
        {
            var project = projectViewModel.Map<Project>();
            project.CreateDate = DateTime.Now;
            project.ModifyDate = DateTime.Now;
            project.CreateUserId = Current.UserId;
            project.ModifyUserId = Current.UserId;
            project.ProjectId = Guid.NewGuid();

            projectService.CreateProject(project);

            contextHelper.SaveChanges();

            // Set the new id back on the view so it can be called post-save
            projectViewModel.ProjectId = project.ProjectId;
        }

        public ProjectViewModel Find(Guid projectId)
        {
            var project = projectService.Find(projectId)
                                                  .Map<ProjectViewModel>();

            if (project.AccountId != null)
                project.AccountIdName = accountService.Find(project.AccountId)?.Name;
            if (project.PrimaryContactId != null)
                project.PrimaryContactIdEmailAddress = contactService.Find(project.PrimaryContactId)?.EmailAddress;
            Hydrate(project);
            return project;
        }

        public void SaveProject(ProjectViewModel projectViewModel)
        {
            var project = projectViewModel.Map<Project>();
            project.ModifyDate = DateTime.Now;
            project.ModifyUserId = Current.UserId;

            var ignore = FieldMappingInspector<Project>.GetAllReadonlyFields(projectViewModel);

            projectService.UpdateProject(project, ignore);

            contextHelper.SaveChanges();
        }

		public void DeleteProject(ProjectViewModel projectViewModel)
        {
            var project = projectViewModel.Map<Project>();

            projectService.DeleteProject(project);

            contextHelper.SaveChanges();
        }
        public string GetAccountIdName(Guid? id)
        {
			return accountService.Find(id)?.Name;
        }
        public string GetPrimaryContactIdEmailAddress(Guid? id)
        {
			return contactService.Find(id)?.EmailAddress;
        }

        public void Hydrate(ProjectViewModel projectViewModel)
        {
            var id = projectViewModel.ProjectId;
            projectViewModel.AuditHistory = GetAuditRecords(id);
            projectViewModel.AccountIdName =     GetAccountIdName(projectViewModel.AccountId);
            projectViewModel.PrimaryContactIdEmailAddress =     GetPrimaryContactIdEmailAddress(projectViewModel.PrimaryContactId);
            projectViewModel.ProjectLogs=     GetProjectLogs(id);
            projectViewModel.Invoices=     GetInvoices(id);
            projectViewModel.Notes=     GetNotes(id);
        }

        public List<ProjectLogViewModel> GetProjectLogs(Guid projectId)
        {
            return projectLogViewModelService.GetAllProjectLogs()
                        .Where(x => x.ProjectId == projectId)
                        .ToList();
        }
        public List<InvoiceViewModel> GetInvoices(Guid projectId)
        {
            return invoiceViewModelService.GetAllInvoices()
                        .Where(x => x.ProjectId == projectId)
                        .ToList();
        }
        public List<NoteViewModel> GetNotes(Guid projectId)
        {
            return noteViewModelService.GetAllNotes()
                        .Where(x => x.ProjectId == projectId)
                        .ToList();
        }
        public List<AuditRecord> GetAuditRecords(Guid projectId)
        {
            return auditService.GetAuditRecords()
                               .Where(x => x.RecordId == projectId && x.RecordType == "Project")
                               .ToList();
        }

    }
}
