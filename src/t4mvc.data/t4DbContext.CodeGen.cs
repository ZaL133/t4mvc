using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using t4mvc.core;

namespace t4mvc.Data
{
    public partial class t4DbContext : IdentityDbContext<t4mvcUser, IdentityRole<Guid>, Guid>
    {
        public t4DbContext (DbContextOptions<t4DbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }
}