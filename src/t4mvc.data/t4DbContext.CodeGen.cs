using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using t4mvc.core;

namespace t4mvc.data
{
    public partial class t4DbContext : IdentityDbContext<t4mvcUser, t4mvcRole, Guid>
    {
        public t4DbContext (DbContextOptions<t4DbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectLog> ProjectLogs { get; set; }
    }
}