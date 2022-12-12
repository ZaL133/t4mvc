using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.scaffolding.EntityDefinition;

namespace t4mvc.scaffolding.templates
{
    public partial class admincontroller
    {
        public string AreaName { get; }
        public Entity Entity { get; }

        public admincontroller(string areaName, Entity entity)
        {
            this.AreaName = areaName;
            this.Entity = entity;
        }
    }
}
