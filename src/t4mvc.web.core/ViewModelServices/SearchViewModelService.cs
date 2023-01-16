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
    /// <summary>
    /// Use this to implement any custom searching. Register this as the global serach type in Program.cs
    /// builder.Services.AddSingleton<ISearchViewModelService, SearchViewModelService>();
    /// </summary>
    public class SearchViewModelService : SearchViewModelServiceBase
    {
        public SearchViewModelService(IAccountService accountService, IContactService contactService, IProjectService projectService) : base(accountService, contactService, projectService)
        {

        }

        public override GlobalSearchResult Search(string searchTerm)
        {
            return SearchInternal(searchTerm, 3);
        }

        public override GlobalSearchResult SearchAll(string searchTerm)
        {
            return SearchInternal(searchTerm, null);
        }

        private GlobalSearchResult SearchInternal(string searchTerm, int? take = null)
        {
            var rv = SearchInternalBase(searchTerm, take);
            // Do any custom serach here 

            return rv;
        }
    }
}
