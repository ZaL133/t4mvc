using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.core
{
    public class AuditRecord
    {
        public Guid AuditRecordId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }
        public Guid RecordId { get; set; }
        // Create, Update, Delete
        public string AuditType { get; set; }
        public string RecordType { get; set; }
        public string ChangedFields { get; set; }
    }
}
