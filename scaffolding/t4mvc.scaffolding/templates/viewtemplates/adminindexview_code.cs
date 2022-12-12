using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.scaffolding.EntityDefinition;

namespace t4mvc.scaffolding.templates.viewtemplates
{
    public partial class adminindexview
    {
        public adminindexview(string areaName, Entity entity)
        {
            AreaName = areaName;
            Entity = entity;
        }

        public string AreaName { get; }
        public Entity Entity { get; }
    }
}
