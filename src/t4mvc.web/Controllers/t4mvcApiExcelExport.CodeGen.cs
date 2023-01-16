using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.Models;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;

namespace t4mvc.web.Controllers
{
    [ApiController]
    [Route("api/export")]
    public partial class ExportExcelController
    {
        readonly It4mvcApiController t4mvcApiController;

        public ExportExcelController(It4mvcApiController t4mvcApiController)
        {
            this.t4mvcApiController = t4mvcApiController;
        }

        [Route("getAccounts")]
        public IActionResult GetAccounts(string cacheKey)
        {
            var request = Current.GetDataTablesParameters(nameof(t4mvcApiController.GetAccounts), cacheKey);
            return ConvertToExcel(request, () => t4mvcApiController.GetAccounts(request, cacheKey), $"Accounts_{DateTime.Now.ToString("yyyyMMdd")}.xlsx");
        }
        [Route("getContacts")]
        public IActionResult GetContacts(string cacheKey)
        {
            var request = Current.GetDataTablesParameters(nameof(t4mvcApiController.GetContacts), cacheKey);
            return ConvertToExcel(request, () => t4mvcApiController.GetContacts(request, cacheKey), $"Contacts_{DateTime.Now.ToString("yyyyMMdd")}.xlsx");
        }
        [Route("getProjects")]
        public IActionResult GetProjects(string cacheKey)
        {
            var request = Current.GetDataTablesParameters(nameof(t4mvcApiController.GetProjects), cacheKey);
            return ConvertToExcel(request, () => t4mvcApiController.GetProjects(request, cacheKey), $"Projects_{DateTime.Now.ToString("yyyyMMdd")}.xlsx");
        }
        [Route("getNotes")]
        public IActionResult GetNotes(string cacheKey)
        {
            var request = Current.GetDataTablesParameters(nameof(t4mvcApiController.GetNotes), cacheKey);
            return ConvertToExcel(request, () => t4mvcApiController.GetNotes(request, cacheKey), $"Notes_{DateTime.Now.ToString("yyyyMMdd")}.xlsx");
        }
    }
}