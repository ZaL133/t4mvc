using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.scaffolding.EntityDefinition;

namespace t4mvc.scaffolding.templates
{
    public partial class dbcontext
    {
        public IEnumerable<Entity> Entities { get; }
        public dbcontext(IEnumerable<Entity> entities)
        {
            this.Entities = entities;
        }
    }
}
