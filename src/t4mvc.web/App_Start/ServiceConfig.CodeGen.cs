using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using t4mvc.core;
using t4mvc.data;
using t4mvc.data.services;
using t4mvc.web.core.viewmodels;
using t4mvc.web.core.viewmodelservices;

namespace t4mvc.web
{
    public static partial class ServiceConfig
    {
	    public static void AddCodeGen(IServiceCollection services)
        {
		    services.AddScoped<IAccountService, AccountService>();
		    services.AddScoped<IAccountViewModelService, AccountViewModelService>();
        }
    }
}
