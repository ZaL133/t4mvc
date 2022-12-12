using t4mvc.core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace t4mvc.data.Services
{
    public partial interface IAccountService
    {
        Account Find(params object[] keyValues);
        IQueryable<Account> GetAllAccounts();
        void CreateAccount(Account account);
        void UpdateAccount(Account account, IEnumerable<string> ignore);
		void DeleteAccount(Account account);
    }

    public partial class AccountService : IAccountService
    {
        private readonly t4DbContext context;
        public AccountService(t4DbContext dbContext)
        {
            this.context = dbContext;
        }

        public void CreateAccount(Account account)
        {
            this.context.Accounts.Add(account);
        }

        public Account Find(params object[] keyValues)
        {
            return this.context.Accounts.Find(keyValues);
        }

        public IQueryable<Account> GetAllAccounts()
        {
            return this.context.Accounts.AsQueryable();
        }

        public void UpdateAccount(Account account, IEnumerable<string> ignore)
        {
            this.context.Accounts.Attach(account);

            var entry       = this.context.Entry(account);
            entry.State     = EntityState.Modified;

            foreach(var prop in ignore)
            {
                entry.Property(prop).IsModified = false;
            }
        }

		public void DeleteAccount(Account account)
        {
            this.context.Accounts.Remove(account);
        }
    }

}