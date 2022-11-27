using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.scaffolding.EntityDefinition
{
    public class Layout
    {
        public List<Section> Sections = new List<Section>();
        public List<Row> Rows = new List<Row>();
    }

    public class Section
    {
        public bool Expanded { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public List<Row> Rows { get; set; } = new List<Row>();
    }

    public class Row
    {
        public List<LayoutField> Fields { get; set; } = new List<LayoutField>();
    }

    public class LayoutField
    {
        public int Columns { get; set; } = 0;
        public Field Field { get; set; }
        public LayoutField(Field field)
        {
            this.Field = field;
        }
    }
}
