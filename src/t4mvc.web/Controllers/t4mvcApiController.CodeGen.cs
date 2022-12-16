using System;
using System.Collections.Generic;
using System.Linq;
using t4mvc.web.core;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.Models;
using t4mvc.web.core.ViewModels;
using t4mvc.web.core.ViewModelServices;
using Microsoft.AspNetCore.Mvc;
namespace t4mvc.Web.Controllers
{
    public partial interface It4mvcApiController
    {
        DataTablesResultsBase GetAccounts(DataTablesRequestBase request, string cacheKey);
    }

    [Route("api")]
	public partial class t4mvcApiController : t4mvcController, It4mvcApiController
	{
        IServiceProvider serviceProvider;
        IAccountViewModelService accountViewModelService;

	public t4mvcApiController(IServiceProvider serviceProvider, IAccountViewModelService accountViewModelService)
    {
        this.serviceProvider = serviceProvider;        this.accountViewModelService = accountViewModelService;
    }

        [Route("getaccounts")]
        public DataTablesResultsBase GetAccounts(DataTablesRequestBase request, string cacheKey)
        {
            Current.SetDataTablesParameters(nameof(GetAccounts), cacheKey, request);

            var response = new DataTablesResultsBase() { draw = request.Draw };

            var queryBase = accountViewModelService.GetAllAccounts();

            if (request.Search != null && request.Search.Value != null)
            {
                var s = request.Search.Value;
                queryBase = queryBase.Where(x => x.Name.Contains(s));
            }
            var query = queryBase.Sort(request)
                                 .Filter(request);

            var data = query.Skip(request.Start)
                            .Take(request.Length)
                            .ToList();

            var totalRecords            = queryBase.Count();
            response.recordsTotal       = totalRecords;
            response.recordsFiltered    = totalRecords;
            response.data               = data;

            return response;

        }

        // Select2 field
        [HttpGet]
        [Route("select2/getaccounts")]
        public IEnumerable<AccountViewModel> Select2Accounts(Select2SearchParameter searchParameter)
        {
            var records = accountViewModelService.GetAllAccounts();

            if (searchParameter != null && searchParameter.Query != null)
            {
                records = records.Where(x => x.Name.StartsWith(searchParameter.Query));
            }

            return records.ToList();
        }

	}
}
