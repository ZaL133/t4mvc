using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.web.core.Infrastructure
{
    public static class Settings
    {
        private const string READONLYPREFIX = "prop-readOnly-";
        public static int DATATABLEMAXLENGTH { get; set; } = 1000000;
        public static void SetReadonlyProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            var key = READONLYPREFIX + propertyName;
            Current.Context.Items[key] = true;
        }

        public static class Icon
        {
            // Misc icons
            public static HtmlString SearchIcon => GetIcon("fa-search");
            public static HtmlString DetailsIcon => GetIcon26("feather-file-text");

            private static HtmlString GetIcon(string icon)
            {
                if (icon.StartsWith("feather"))
                {
                    var feather = icon.Split("feather-", StringSplitOptions.None);
                    return new HtmlString($"<i data-feather=\"{feather[1]}\"></i>");
                }
                else
                {
                    return new HtmlString(string.Format("<i class='fa {0}'></i>", icon));
                }
            }
            public static HtmlString GetIcon26(string icon)
            {
                if (icon.StartsWith("feather"))
                {
                    var feather = icon.Split("feather-", StringSplitOptions.None);
                    return new HtmlString($"<i data-feather=\"{feather[1]}\"></i>");
                }
                return (icon.StartsWith("fa-")) ? GetIcon(icon) : new HtmlString($"<i class='i i-26 {icon}'></i>");
            }

            public static HtmlString GetIcon20(string icon)
            {
                if (icon.StartsWith("feather"))
                {
                    var feather = icon.Split("feather-", StringSplitOptions.None);
                    return new HtmlString($"<i data-feather=\"{feather[1]}\"></i>");
                }
                return (icon.StartsWith("fa-")) ? GetIcon(icon) : new HtmlString($"<i class='i i-20 {icon}'></i>");
            }
        }
    }
}
