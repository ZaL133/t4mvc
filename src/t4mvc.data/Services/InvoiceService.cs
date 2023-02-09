using t4mvc.core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace t4mvc.data.Services
{
    public partial interface IInvoiceService
    {
        Invoice Find(params object[] keyValues);
        IQueryable<Invoice> GetAllInvoices();
        void CreateInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice, IEnumerable<string> ignore);
		void DeleteInvoice(Invoice invoice);
    }

    public partial class InvoiceService : IInvoiceService
    {
        private readonly t4DbContext context;
        public InvoiceService(t4DbContext dbContext)
        {
            this.context = dbContext;
        }

        public void CreateInvoice(Invoice invoice)
        {
            // Auditing 
            var auditRecord     = invoice.GetCreateAuditRecord(invoice.InvoiceId, invoice.CreateUserId);
            this.context.AuditRecord.Add(auditRecord); 
            this.context.Invoices.Add(invoice);
        }

        public Invoice Find(params object[] keyValues)
        {
            return this.context.Invoices.Find(keyValues);
        }

        public IQueryable<Invoice> GetAllInvoices()
        {
            return this.context.Invoices.AsQueryable();
        }

        public void UpdateInvoice(Invoice invoice, IEnumerable<string> ignore)
        {
            // Auditing 
            var oldRecord       = this.context.Invoices.AsNoTracking().Single(x => x.InvoiceId == invoice.InvoiceId);
            var auditRecord     = oldRecord.GetUpdateAuditRecord(invoice, ignore);
            this.context.AuditRecord.Add(auditRecord); 


            this.context.Invoices.Attach(invoice);

            var entry       = this.context.Entry(invoice);
            entry.State     = EntityState.Modified;

            foreach(var prop in ignore)
            {
                entry.Property(prop).IsModified = false;
            }
        }

		public void DeleteInvoice(Invoice invoice)
        {
            // Auditing 
            var oldRecord       = this.context.Invoices.AsNoTracking().Single(x => x.InvoiceId == invoice.InvoiceId);
            var auditRecord     = oldRecord.GetDeleteAuditRecord(invoice.InvoiceId, invoice.ModifyUserId);
            this.context.AuditRecord.Add(auditRecord); 
            this.context.Invoices.Remove(invoice);
        }
    }

}