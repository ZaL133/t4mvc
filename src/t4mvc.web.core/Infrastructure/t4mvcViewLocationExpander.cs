using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.web.core.Infrastructure
{
    public class t4mvcViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //{2} is area, {1} is controller,{0} is the action
            string location     = "/Views/{1}/CodeGen/{0}.cshtml";
            var rv              = viewLocations.ToList();

            rv.Add(location);
            return rv;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(t4mvcViewLocationExpander);
        }
    }
}
