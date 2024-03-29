using Microsoft.AspNetCore.Mvc;

namespace t4mvc.web.Controllers
{
    public partial class ReportController
    {
        public ActionResult GetAccounts(string cacheKey)
        {
            return GetPreview(methodName:   nameof(It4mvcApiController.GetAccounts),
                              cacheKey:     cacheKey,
                              dataMethod:   (request) => t4mvcApiController.GetAccounts(request, cacheKey),
                              reportName:   "Account");
        }
        public ActionResult GetContacts(string cacheKey)
        {
            return GetPreview(methodName:   nameof(It4mvcApiController.GetContacts),
                              cacheKey:     cacheKey,
                              dataMethod:   (request) => t4mvcApiController.GetContacts(request, cacheKey),
                              reportName:   "Contact");
        }
        public ActionResult GetProjects(string cacheKey)
        {
            return GetPreview(methodName:   nameof(It4mvcApiController.GetProjects),
                              cacheKey:     cacheKey,
                              dataMethod:   (request) => t4mvcApiController.GetProjects(request, cacheKey),
                              reportName:   "Project");
        }
        public ActionResult GetProjectLogs(string cacheKey)
        {
            return GetPreview(methodName:   nameof(It4mvcApiController.GetProjectLogs),
                              cacheKey:     cacheKey,
                              dataMethod:   (request) => t4mvcApiController.GetProjectLogs(request, cacheKey),
                              reportName:   "ProjectLog");
        }
        public ActionResult GetInvoices(string cacheKey)
        {
            return GetPreview(methodName:   nameof(It4mvcApiController.GetInvoices),
                              cacheKey:     cacheKey,
                              dataMethod:   (request) => t4mvcApiController.GetInvoices(request, cacheKey),
                              reportName:   "Invoice");
        }
        public ActionResult GetNotes(string cacheKey)
        {
            return GetPreview(methodName:   nameof(It4mvcApiController.GetNotes),
                              cacheKey:     cacheKey,
                              dataMethod:   (request) => t4mvcApiController.GetNotes(request, cacheKey),
                              reportName:   "Note");
        }

    }
}