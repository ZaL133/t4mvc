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

    public class SearchViewModelServiceBase : ISearchViewModelService
    {
        private readonly IAccountService accountService;
        private readonly IContactService contactService;

        public SearchViewModelServiceBase(IAccountService accountService, IContactService contactService)
        {
            this.accountService = accountService;
            this.contactService = contactService;
        }

        public virtual GlobalSearchResult Search(string searchTerm)
        {
            return SearchInternalBase(searchTerm, 3);
        }

        public virtual GlobalSearchResult SearchAll(string searchTerm)
        {
            return SearchInternalBase(searchTerm, null);
        }

        protected GlobalSearchResult SearchInternalBase(string searchTerm, int? take = null)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return new GlobalSearchResult();

            var rv              = new GlobalSearchResult();
            var requestor       = Current.UserId;
            bool isPossibleId   = Guid.TryParse(searchTerm, out Guid searchId);

            IQueryable<Account> accounts;
            IQueryable<Contact> contacts;

            if (isPossibleId)
            {
                accounts   = accountService.GetAllAccounts().Where(x => x.AccountId == searchId);
                contacts   = contactService.GetAllContacts().Where(x => x.ContactId == searchId);
            }
            else
            {
                accounts   = accountService.GetAllAccounts().Where(x => x.Name.Contains(searchTerm));
                contacts   = contactService.GetAllContacts().Where(x => x.FirstName.StartsWith(searchTerm) || x.LastName.StartsWith(searchTerm) || x.EmailAddress.StartsWith(searchTerm));
            }

            if (take.HasValue)
            {
                accounts = accounts.Take(take.Value);
                contacts = contacts.Take(take.Value);
            }

            if (accounts.Any())
            {
                rv.Categories.Add(new SearchResultCategory
                {
                    Name    = "Account",
                    Icon    = "<i data-feather=\"home\"></i>",
                    Results = accounts.AsEnumerable().Select(x => new SearchResultItem { Title = $"{x.Name}", Url = "/crm/account/details/" + x.AccountId }).ToList()
                });
            }

            if (contacts.Any())
            {
                rv.Categories.Add(new SearchResultCategory
                {
                    Name    = "Contact",
                    Icon    = "<i data-feather=\"user\"></i>",
                    Results = contacts.AsEnumerable().Select(x => new SearchResultItem { Title = $"{x.EmailAddress}", Url = "/crm/contact/details/" + x.ContactId }).ToList()
                });
            }

            return rv;
        }
    }
}
