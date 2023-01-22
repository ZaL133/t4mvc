using System;
using System.Collections.Generic;
using System.Linq;
using t4mvc.web.core;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.Models;
using t4mvc.web.core.ViewModels;
using t4mvc.web.core.ViewModelServices;
using Microsoft.AspNetCore.Mvc;
namespace t4mvc.web.Controllers
{
    public partial interface It4mvcApiController
    {
        DataTablesResultsBase GetAccounts(DataTablesRequestBase request, string cacheKey);
        DataTablesResultsBase GetContacts(DataTablesRequestBase request, string cacheKey);
        DataTablesResultsBase GetProjects(DataTablesRequestBase request, string cacheKey);
        DataTablesResultsBase GetNotes(DataTablesRequestBase request, string cacheKey);
    }

    [Route("api")]
	public partial class t4mvcApiController : t4mvcController, It4mvcApiController
	{
        IServiceProvider serviceProvider;
        IAccountViewModelService accountViewModelService;
        IContactViewModelService contactViewModelService;
        IProjectViewModelService projectViewModelService;
        INoteViewModelService noteViewModelService;

	public t4mvcApiController(IServiceProvider serviceProvider, IAccountViewModelService accountViewModelService,IContactViewModelService contactViewModelService,IProjectViewModelService projectViewModelService,INoteViewModelService noteViewModelService)
    {
        this.serviceProvider = serviceProvider;        this.accountViewModelService = accountViewModelService;
        this.contactViewModelService = contactViewModelService;
        this.projectViewModelService = projectViewModelService;
        this.noteViewModelService = noteViewModelService;
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
        [Route("getcontacts")]
        public DataTablesResultsBase GetContacts(DataTablesRequestBase request, string cacheKey)
        {
            Current.SetDataTablesParameters(nameof(GetContacts), cacheKey, request);

            var response = new DataTablesResultsBase() { draw = request.Draw };

            var queryBase = contactViewModelService.GetAllContacts();

            if (request.Search != null && request.Search.Value != null)
            {
                var s = request.Search.Value;
                queryBase = queryBase.Where(x => x.FirstName.StartsWith(s) || x.LastName.StartsWith(s) || x.EmailAddress.StartsWith(s));
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
        [Route("select2/getcontacts")]
        public IEnumerable<ContactViewModel> Select2Contacts(Select2SearchParameter searchParameter)
        {
            var records = contactViewModelService.GetAllContacts();

            if (searchParameter != null && searchParameter.Query != null)
            {
                records = records.Where(x => x.EmailAddress.StartsWith(searchParameter.Query));
            }

            return records.ToList();
        }
        [Route("getprojects")]
        public DataTablesResultsBase GetProjects(DataTablesRequestBase request, string cacheKey)
        {
            Current.SetDataTablesParameters(nameof(GetProjects), cacheKey, request);

            var response = new DataTablesResultsBase() { draw = request.Draw };

            var queryBase = projectViewModelService.GetAllProjects();

            if (request.Search != null && request.Search.Value != null)
            {
                var s = request.Search.Value;
                queryBase = queryBase.Where(x => x.ProjectName.StartsWith(s) || x.AccountIdName.Contains(s) || x.PrimaryContactIdEmailAddress.Contains(s));
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
        [Route("select2/getprojects")]
        public IEnumerable<ProjectViewModel> Select2Projects(Select2SearchParameter searchParameter)
        {
            var records = projectViewModelService.GetAllProjects();

            if (searchParameter != null && searchParameter.Query != null)
            {
                records = records.Where(x => x.ProjectName.StartsWith(searchParameter.Query));
            }

            return records.ToList();
        }
        [Route("getnotes")]
        public DataTablesResultsBase GetNotes(DataTablesRequestBase request, string cacheKey)
        {
            Current.SetDataTablesParameters(nameof(GetNotes), cacheKey, request);

            var response = new DataTablesResultsBase() { draw = request.Draw };

            var queryBase = noteViewModelService.GetAllNotes();

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


	}
}
