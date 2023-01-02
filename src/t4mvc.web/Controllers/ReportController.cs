using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Composition;
using t4mvc.web.core;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.Models;
using t4mvc.web.core.Reporting;

namespace t4mvc.web.Controllers
{
    public class ReportController : Controller
    {
        private readonly It4mvcApiController t4MvcApiController;

        public ReportController(It4mvcApiController t4mvcApiController) 
        {
            t4MvcApiController = t4mvcApiController;
        }

        [HttpPost]
        public ActionResult GeneratePreview(DataTablesPrintRequest request)
        {
            if (request == null)
            {
                var response            = new ContentResult();
                response.Content        = "<h1>The parameters for this request have expired. Please refresh the page and try again</h1>";
                response.ContentType    = "text/html";

                return response;
            }
            else
            {
                var report = new Report() { Name = request.FileName };
                DataTablesPrintToReportRows(report, request);

                Guid id = Guid.NewGuid();
                Current.PushTemp(id.ToString(), report);

                return Json(new { keyid = id.ToString() });
            }
        }

        public ActionResult Preview(Guid? id, string fileName)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                var report = Current.PopTemp(id.Value.ToString()) as Report;
                if (report == null)
                {
                    var response            = new ContentResult();
                    response.Content        = "<h1>The parameters for this request have expired. Please refresh the page and try again</h1>";
                    response.ContentType    = "text/html";

                    return response;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        report.Name     = fileName;
                        report.Header1  = fileName;
                    }
                    return View("~/Views/Shared/Report.cshtml", report);
                }
            }
        }

        /// <summary>
        /// Internal method for generating the preview
        /// </summary>
        private ActionResult GetPreview(string methodName, string cacheKey, Func<DataTablesRequestBase, DataTablesResultsBase> dataMethod, string reportName, Dictionary<string, Func<object, string>> formatterDictionary = null)
        {
            var request = Current.GetDataTablesParameters(methodName, cacheKey);
            if (request == null)
            {
                return View("~/Views/Shared/ErrorNotification.cshtml", "The request has expired. Please refresh the page and try again");
            }
            request.Length = 100000;
            var data = dataMethod(request);
            var report = new Report() { Name = reportName, Header1 = reportName };

            ConvertToReportRows(report, request, data, formatterDictionary);
            return View("~/Views/Shared/Report.cshtml", report);
        }

        private void ConvertToReportRows(Report report, DataTablesRequestBase request, DataTablesResultsBase result, Dictionary<string, Func<object, string>> formatterDictionary = null)
        {
            var jArray = JObject.FromObject(result)["data"];
            int rowNumber = 1, columnNumber = 1;
            Dictionary<string, ReportColumn> _dic = new Dictionary<string, ReportColumn>();

            foreach (var col in request.Columns)
            {
                var rc = new ReportColumn { ColumnName = col.Data };
                _dic.Add(col.Data, rc);

                report.Columns.Add(rc);
                columnNumber++;
            }

            // reset header
            columnNumber = 1;
            rowNumber++;

            foreach (var jObject in jArray.Children())
            {
                var row = new ReportRow { Values = new Dictionary<string, IReportRowValue>() };

                foreach (var col in request.Columns)
                {
                    var colValue = ((Newtonsoft.Json.Linq.JValue)jObject[col.Data]).Value;
                    var reportVal = formatterDictionary != null && formatterDictionary.ContainsKey(col.Data) ? formatterDictionary[col.Data](colValue) : colValue?.ToString();
                    row.Values.Add(col.Data, new ReportRowValue<string>(reportVal));

                    columnNumber++;
                }

                rowNumber++;
                columnNumber = 1;

                report.Data.Add(row);
            }

        }

        /// <summary>
        /// Updates the report parameter to include data from the print request. <br>
        /// This is print request on a client side data table.
        /// </summary>
        /// <param name="report">The report to update</param>
        /// <param name="request">The print request - containing data</param>
        private void DataTablesPrintToReportRows(Report report, DataTablesPrintRequest request)
        {
            report.Data = new List<ReportRow>(request.Data?[0].Length ?? 0);

            int column = 0;

            // Headers
            foreach (var col in request.Columns)
            {
                var dCol = new ReportColumn() { ColumnName = col };
                report.Columns.Add(dCol);
                column++;
            }

            if (request.Data != null)
            {
                foreach (var dataRow in request.Data)
                {
                    var reportRow = new ReportRow(new Dictionary<string, IReportRowValue>());
                    report.Data.Add(reportRow);

                    for (var colPos = 0; colPos < request.Columns.Length; colPos++)
                    {
                        reportRow.Values.Add(request.Columns[colPos], new ReportRowValue<string>(Utils.FormatDataTablesValue(dataRow[colPos], request.ColumnDefs[colPos])));
                    }
                }
            }
        }
    }
}
