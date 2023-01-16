using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.core;

namespace t4mvc.data.Services
{
    public partial interface IAuditService
    {
        IQueryable<AuditRecord> GetAuditRecords();
    }

    public partial class AuditService : IAuditService
    {
        private readonly t4DbContext t4DbContext;

        public AuditService(t4DbContext t4DbContext)
        {
            this.t4DbContext = t4DbContext;
        }

        public IQueryable<AuditRecord> GetAuditRecords()
        {
            return t4DbContext.AuditRecord.AsQueryable();
        }
    }
}
