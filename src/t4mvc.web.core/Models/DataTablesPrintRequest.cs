using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.web.core.Models
{
    /// <summary>
    /// This is for printing and exporting a datatables object that is done client side
    /// </summary>
    public class DataTablesPrintRequest
    {
        // Download file name / Report Title
        public string FileName { get; set; }
        // An array of the column names
        public string[] Columns { get; set; }
        // An array of the render config function names. 
        public string[] ColumnDefs { get; set; }
        // A multi-dimensional array containing all the data. Organized by [rows][column]
        public string[][] Data { get; set; }
    }
}
