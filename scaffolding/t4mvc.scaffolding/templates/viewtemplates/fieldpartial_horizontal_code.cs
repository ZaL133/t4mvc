using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.scaffolding.EntityDefinition;

namespace t4mvc.scaffolding.templates.viewtemplates
{
    public partial class fieldpartial_horizontal
    {

        public fieldpartial_horizontal(Field field, int columns)
        {
            Field           = field;
            this.columns    = columns;
        }

        public Field Field { get; }
        public int LabelColumns => columns < 6 ? 4 : 2;
        public int FieldColumns => 12 - LabelColumns;

        private int columns;
    }
}
