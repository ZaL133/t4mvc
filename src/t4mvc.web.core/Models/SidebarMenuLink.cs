using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using t4mvc.web.core.Infrastructure;

namespace t4mvc.web.core.Models
{
    public class SidebarMenuLink
    {
        public string Url { get; set; }
        public HtmlString Icon { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }

        public HtmlString GetLink()
        {
            return string.IsNullOrEmpty(this.Url)
                    ? new HtmlString($"<span>{this.Icon?.ToString()} {this.Text}</span>")
                    : new HtmlString($"<a href='{this.Url}'>{this.Icon?.ToString()} {this.Text}</a>");
        }

        public IList<SidebarMenuLink> Children { get; set; } = new List<SidebarMenuLink>();
    }
}
