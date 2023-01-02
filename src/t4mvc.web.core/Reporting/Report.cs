using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.web.core.Reporting
{
    public class Report
    {
        public string Name { get; set; }
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public string CustomCss { get; set; }
        public List<ReportColumn> Columns { get; set; } = new List<ReportColumn>();
        public List<ReportRow> Data { get; set; } = new List<ReportRow>();

        // Grouping stuff
        public string GroupBy { get; set; }
        public List<IGrouping<string, ReportRow>> GroupedData = new List<IGrouping<string, ReportRow>>();

        public decimal? SumColumn(IEnumerable<ReportRow> rows, string columnName)
        {
            var values = rows.Select(x => x.Values[columnName].GetValue() as decimal?);
            return values.Sum();
        }
    }

    public class ReportColumn
    {
        public string ColumnTitle { get; set; }
        public int ColumnNumber { get; set; }
        public string ColumnName { get; set; }
        public string ColumnClass { get; set; }
        public string RowClass { get; set; }
        public bool CalculateTotal { get; set; }
        public string TotalRowClass { get; set; }
        public string TotalRowText { get; set; }
        public bool GroupingKey { get; set; }
    }

    public class ReportRow
    {
        public ReportRow()
        {

        }
        public ReportRow(Dictionary<string, IReportRowValue> values)
        {
            Values = values;
        }
        public Dictionary<string, IReportRowValue> Values { get; set; }
    }

    public interface IReportRowValue
    {
        ReportColumn Column { get; set; }
        object GetValue();
    }

    public class ReportRowValue<T> : IReportRowValue
    {
        public ReportRowValue(T val)
        {
            this.Value = val;
        }
        public ReportRowValue(T val, ReportColumn column)
        {
            this.Value = val;
            this.Column = column;
        }
        public ReportColumn Column { get; set; }
        public T Value { get; set; }
        public object GetValue()
        {
            return this.Value;
        }
    }
}
