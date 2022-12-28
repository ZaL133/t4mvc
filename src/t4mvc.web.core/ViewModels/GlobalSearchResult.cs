using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.web.core.ViewModels
{
    public class GlobalSearchResult
    {
        public List<SearchResultCategory> Categories { get; set; } = new List<SearchResultCategory>();
    }

    public class SearchResultCategory
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public List<SearchResultItem> Results { get; set; } = new List<SearchResultItem>();
    }

    public class SearchResultItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
