using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.web.core.Models.ModelBinders;

namespace t4mvc.web.core.Models
{
    [ModelBinder(typeof(DataTablesRequestModelBinder))]
    public class DataTablesRequestBase
    {
        // 
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DataTablesSearch Search { get; set; }
        public DataTableOrder[] Order { get; set; }
        public DataTableColumn[] Columns { get; set; }
    }

    public class DataTablesSearch
    {
        public string Value { get; set; }
        public bool RegEx { get; set; }
    }

    public class DataTableOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class DataTableColumn
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DataTablesSearch Search { get; set; }
    }

}
