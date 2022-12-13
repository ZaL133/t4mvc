using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.scaffolding.EntityDefinition;

namespace t4mvc.scaffolding.templates.viewtemplates
{
    public partial class admindetails
    {
        public admindetails(string area, Entity entity)
        {
            Area    = area;
            Entity  = entity;
        }

        public string Area { get; }
        public Entity Entity { get; }
        public IEnumerable<ChildReferenceEntity> tabs
        {
            get
            {
                return this.Entity.ChildReferences.Where(x => x.Tabbed == true);
            }
        }
    }
}
