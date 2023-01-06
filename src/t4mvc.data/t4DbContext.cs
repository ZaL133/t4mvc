using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using t4mvc.core;

namespace t4mvc.data
{
    public partial class t4DbContext
    {
        public DbSet<AuditRecord> AuditRecord { get; set; }
    }
}