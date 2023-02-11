using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using t4mvc.core;
using t4mvc.data;
using t4mvc.data.Services;
using t4mvc.web.core.ViewModels;
using t4mvc.web.core.ViewModelServices;

namespace t4mvc.web
{
    public static partial class ServiceConfig
    {
	    public static void AddCodeGen(IServiceCollection services)
        {
		    services.AddScoped<IAccountService, AccountService>();
		    services.AddScoped<IAccountViewModelService, AccountViewModelService>();		    services.AddScoped<IContactService, ContactService>();
		    services.AddScoped<IContactViewModelService, ContactViewModelService>();		    services.AddScoped<IInvoiceService, InvoiceService>();
		    services.AddScoped<IInvoiceViewModelService, InvoiceViewModelService>();		    services.AddScoped<INoteService, NoteService>();
		    services.AddScoped<INoteViewModelService, NoteViewModelService>();		    services.AddScoped<IProjectService, ProjectService>();
		    services.AddScoped<IProjectViewModelService, ProjectViewModelService>();		    services.AddScoped<IProjectLogService, ProjectLogService>();
		    services.AddScoped<IProjectLogViewModelService, ProjectLogViewModelService>();
        }
    }
}
