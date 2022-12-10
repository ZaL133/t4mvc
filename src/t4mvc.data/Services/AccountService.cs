using t4mvc.core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace t4mvc.Data.Services
{
    public partial interface IAccountService
    {
        Account Find(params object[] keyValues);
        IQueryable<Account> GetAllAccounts();
        void CreateAccount(Account account);
        void UpdateAccount(Account account, IEnumerable<string> ignore);
		void DeleteAccount(Account account);
    }
}