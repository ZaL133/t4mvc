using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.web.core.Models
{
    public class DataTablesResultsBase
    {
        public static DataTablesResultsBase FromRequest<T>(DataTablesRequestBase request, IQueryable<T> search, bool autoFilter)
        {
            var response = new DataTablesResultsBase { draw = request.Draw };

            var query = search.Sort(request);
            if (autoFilter) query = query.Filter(request);
            var totalRecords = query.Count();

            var data = query.Skip(request.Start)
                            .Take(request.Length)
                            .ToList();

            response.data = data;
            response.recordsTotal = totalRecords;
            response.recordsFiltered = totalRecords;

            return response;
        }

        private DataTablesRequestBase _request;
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public object data { get; set; }
        public string error { get; set; }

        public DataTablesResultsBase()
        {

        }
    }
}
