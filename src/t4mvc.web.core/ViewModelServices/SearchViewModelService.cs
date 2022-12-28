using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.core;
using t4mvc.data.Services;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.ViewModels;

namespace t4mvc.web.core.ViewModelServices
{
    public interface ISearchViewModelService
    {
        GlobalSearchResult Search(string searchTerm);
        GlobalSearchResult SearchAll(string searchTerm);
    }
    public class SearchViewModelService : ISearchViewModelService
    {
        private readonly IAccountService accountService;
        private readonly IContactService contactService;

        public SearchViewModelService(IAccountService accountService, IContactService contactService)
        {
            this.accountService = accountService;
            this.contactService = contactService;
        }

        public IAccountService AccountService => accountService;

        public GlobalSearchResult Search(string searchTerm)
        {
            return SearchInternal(searchTerm, 3);
        }

        public GlobalSearchResult SearchAll(string searchTerm)
        {
            return SearchInternal(searchTerm, null);
        }

        private GlobalSearchResult SearchInternal(string searchTerm, int? take = null)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return new GlobalSearchResult();

            var rv              = new GlobalSearchResult();
            var requestor       = Current.UserId;
            bool isPossibleId   = Guid.TryParse(searchTerm, out Guid searchId);

            IQueryable<Account> accounts;
            IQueryable<Contact> contacts;

            if (isPossibleId)
            {
                accounts    = AccountService.GetAllAccounts().Where(x => x.AccountId == searchId);
                contacts    = contactService.GetAllContacts().Where(x => x.ContactId == searchId);
            }
            else
            {
                accounts    = AccountService.GetAllAccounts().Where(x => x.Name.StartsWith(searchTerm));
                contacts    = contactService.GetAllContacts().Where(x => x.FirstName.StartsWith(searchTerm) || x.LastName.StartsWith(searchTerm) || x.EmailAddress.StartsWith(searchTerm));
            }

            if (take.HasValue)
            {
                accounts    = accounts.Take(take.Value);
                contacts    = contacts.Take(take.Value);
            }

            if (contacts.Any())
            {
                rv.Categories.Add(new SearchResultCategory
                {
                    Name    = "Contacts",
                    Icon    = "<i data-feather=\"user\"></i>",
                    Results = contacts.AsEnumerable().Select(x => new SearchResultItem { Title = $"{x.FirstName} {x.LastName}", Url = "/crm/contacts/details/" + x.ContactId }).ToList()
                });
            }

            if (accounts.Any())
            {
                rv.Categories.Add(new SearchResultCategory
                {
                    Name        = "Accounts",
                    Icon        = "<i data-feather=\"home\"></i>",
                    Results     = accounts.AsEnumerable().Select(x => new SearchResultItem { Title = $"{x.Name}", Url = "/crm/account/details/" + x.AccountId }).ToList()
                });
            }

            return rv;
        }
    }
}
