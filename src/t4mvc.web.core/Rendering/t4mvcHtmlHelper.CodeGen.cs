using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq;
using t4mvc.web.core.Models;
using t4mvc.web.core.Infrastructure;



namespace t4mvc.Web.Core.Rendering
{
    public static partial class t4mvcHtmlHelpers
    {
	    public static void AddCodeGen(UrlHelper url, SidebarMenuModel model)
        {
//			SidebarMenuLink parent = null; 

			// Area: crm
//			parent = model.MenuLinks.FirstOrDefault(x => x.Text == "crm");
//			if (parent == null)
//			{
//				parent = new SidebarMenuLink { Url = "/crm", Text = "crm"};
//				model.MenuLinks.Add(parent);
//			}

//            if (SecurityHelper.HasSecurityLevel(SecurityLevel.crm))
//    //            parent.Children.Add(new SidebarMenuLink() { Url = url.Action("Index", "Account", new { Area = "crm"}), Icon = Settings.Icon.GetIcon20("feather-home"), Text = "Account"});
//            parent.Children.Add(new SidebarMenuLink() { Url = url.Action("Index", "Contact", new { Area = "crm"}), Icon = Settings.Icon.GetIcon20("feather-user"), Text = "Contact"});


			// Area: consulting
//			parent = model.MenuLinks.FirstOrDefault(x => x.Text == "consulting");
//			if (parent == null)
//			{
//				parent = new SidebarMenuLink { Url = "/consulting", Text = "consulting"};
//				model.MenuLinks.Add(parent);
//			}
//            parent.Children.Add(new SidebarMenuLink() { Url = url.Action("Index", "Invoice", new { Area = "consulting"}), Icon = Settings.Icon.GetIcon20("feather-file-text"), Text = "Invoice"});
//            parent.Children.Add(new SidebarMenuLink() { Url = url.Action("Index", "Project", new { Area = "consulting"}), Icon = Settings.Icon.GetIcon20("feather-archive"), Text = "Project"});

        }
    }
}