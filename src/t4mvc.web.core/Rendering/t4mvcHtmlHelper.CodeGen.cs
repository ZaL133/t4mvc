using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using t4mvc.web.core.Models;
using t4mvc.web.core.Infrastructure;



namespace t4mvc.web.core.Rendering
{
    public static partial class t4mvcHtmlHelper
    {
	    public static void AddCodeGen(IUrlHelper url, SidebarMenuModel model)
        {
			SidebarMenuLink parent = null; 
			// Area: crm
			parent = model.MenuLinks.FirstOrDefault(x => x.Text == "crm");
			if (parent == null)
			{
				parent = new SidebarMenuLink { Url = "/crm", Text = "crm"};
				model.MenuLinks.Add(parent);
			}
            parent.Children.Add(new SidebarMenuLink() { Url = url.Action("Index", "Account", new { Area = "crm"}), Icon = Settings.Icon.GetIcon20("feather-home"), Text = "Account"});
            parent.Children.Add(new SidebarMenuLink() { Url = url.Action("Index", "Contact", new { Area = "crm"}), Icon = Settings.Icon.GetIcon20("feather-user"), Text = "Contact"});
        }
    }
}