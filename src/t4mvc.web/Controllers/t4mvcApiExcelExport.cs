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
using t4mvc.web.core;

namespace t4mvc.web.Controllers
{
    public partial class ExportExcelController : ControllerBase
    {
        internal IActionResult ConvertToExcel(DataTablesRequestBase request, Func<DataTablesResultsBase> data, string fileName, Dictionary<string, string> numberFormat = null, Dictionary<string, Func<object, string>> formatters = null)
        {
            if (request == null)
            {
                var content = @"<h1>The parameters for this request have expired. Please refresh the page and try again</h1>
    <script>
        if (window.opener && window.opener.toastr) {
            window.opener.toastr.error('The parameters for this request have expired. Please refresh the page and try again');
            window.close();
        }
    </script>";

                return new ContentResult { Content = content, StatusCode = 500 };
            }
            else
            {
                request.Start = 0;
                request.Length = 1000000;

                var result = data();
                var resultFile = ResultToExcel(request, result, numberFormat);

                return File(resultFile, Consts.EXCELMIMETYPE, fileName);
            }
        }

        private byte[] ResultToExcel(DataTablesRequestBase request, DataTablesResultsBase result, Dictionary<string, string> numberFormat = null, Dictionary<string, Func<object, string>> formatters = null)
        {
            using (var ms = new MemoryStream())
            {
                using (var package = new ExcelPackage())
                {
                    var workbook = package.Workbook;
                    var sheet = workbook.Worksheets.Add("Results");

                    workbook.Styles.CreateNamedStyle("default");

                    var jArray = JObject.FromObject(result)["data"];

                    int i = 1, j = 1;

                    // Headers
                    sheet.Row(i).Style.Font.Bold = true;
                    foreach (var col in request.Columns)
                    {
                        sheet.Cells[i, j].Value = col.Data;
                        j++;
                    }

                    // reset header
                    j = 1;
                    i++;

                    foreach (var jObject in jArray.Children())
                    {
                        foreach (var col in request.Columns)
                        {
                            var colValue = ((Newtonsoft.Json.Linq.JValue)jObject[col.Data]).Value;

                            // Handle a custom formatter
                            // This will always convert to a string
                            if (formatters != null && formatters.ContainsKey(col.Data))
                            {
                                sheet.Cells[i, j].Value = formatters[col.Data](colValue);
                            }
                            else
                            {
                                sheet.Cells[i, j].Value = colValue;
                            }

                            // Format all DateTime's the same way
                            if (colValue is DateTime || colValue is DateTime?)
                            {
                                if (sheet.Column(j).Style.Numberformat.Format != System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)
                                    sheet.Column(j).Style.Numberformat.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                            }

                            // If a number format is provided (e.g. Currency) set the format here.
                            if (numberFormat != null && numberFormat.ContainsKey(col.Data))
                            {
                                sheet.Column(j).Style.Numberformat.Format = numberFormat[col.Data];
                            }
                            j++;
                        }

                        i++;
                        j = 1;
                    }

                    // Fix up columns
                    sheet.Cells.AutoFitColumns();

                    return package.GetAsByteArray();
                }
            }
        }
    }
}