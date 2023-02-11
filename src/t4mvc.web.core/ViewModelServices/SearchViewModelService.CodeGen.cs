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
        private readonly IProjectService projectService;
        private readonly IProjectLogService projectLogService;
        private readonly IInvoiceService invoiceService;

        public SearchViewModelServiceBase(IAccountService accountService, IContactService contactService, IProjectService projectService, IProjectLogService projectLogService, IInvoiceService invoiceService)
        {
            this.accountService = accountService;
            this.contactService = contactService;
            this.projectService = projectService;
            this.projectLogService = projectLogService;
            this.invoiceService = invoiceService;
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
            IQueryable<Project> projects;
            IQueryable<ProjectLog> projectLogs;
            IQueryable<Invoice> invoices;

            if (isPossibleId)
            {

                accounts   = accountService.GetAllAccounts().Where(x => x.AccountId == searchId);
                contacts   = contactService.GetAllContacts().Where(x => x.ContactId == searchId);
                projects   = projectService.GetAllProjects().Where(x => x.ProjectId == searchId);
                projectLogs   = projectLogService.GetAllProjectLogs().Where(x => x.ProjectLogId == searchId);
                invoices   = invoiceService.GetAllInvoices().Where(x => x.InvoiceId == searchId);
            }
            else
            {

                accounts   = accountService.GetAllAccounts().Where(x => x.Name.Contains(searchTerm));
                contacts   = contactService.GetAllContacts().Where(x => x.FirstName.StartsWith(searchTerm) || x.LastName.StartsWith(searchTerm) || x.EmailAddress.StartsWith(searchTerm));
                projects   = projectService.GetAllProjects().Where(x => x.ProjectName.StartsWith(searchTerm));
                projectLogs   = projectLogService.GetAllProjectLogs().Where(x => x.EntryName.StartsWith(searchTerm));
                invoices   = invoiceService.GetAllInvoices().Where(x => x.InvoiceName.StartsWith(searchTerm));
            }

            if (take.HasValue)
            {

                accounts = accounts.Take(take.Value);
                contacts = contacts.Take(take.Value);
                projects = projects.Take(take.Value);
                projectLogs = projectLogs.Take(take.Value);
                invoices = invoices.Take(take.Value);
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

            if (projects.Any())
            {
                rv.Categories.Add(new SearchResultCategory
                {
                    Name    = "Project",
                    Icon    = "<i data-feather=\"archive\"></i>",
                    Results = projects.AsEnumerable().Select(x => new SearchResultItem { Title = $"{x.ProjectName}", Url = "/consulting/project/details/" + x.ProjectId }).ToList()
                });
            }

            if (projectLogs.Any())
            {
                rv.Categories.Add(new SearchResultCategory
                {
                    Name    = "ProjectLog",
                    Icon    = "<i class=\"fa-building\"></i>",
                    Results = projectLogs.AsEnumerable().Select(x => new SearchResultItem { Title = $"{x.EntryName}", Url = "/consulting/projectlog/details/" + x.ProjectLogId }).ToList()
                });
            }

            if (invoices.Any())
            {
                rv.Categories.Add(new SearchResultCategory
                {
                    Name    = "Invoice",
                    Icon    = "<i data-feather=\"file-text\"></i>",
                    Results = invoices.AsEnumerable().Select(x => new SearchResultItem { Title = $"{x.InvoiceName}", Url = "/consulting/invoice/details/" + x.InvoiceId }).ToList()
                });
            }

            return rv;
        }
    }
}
