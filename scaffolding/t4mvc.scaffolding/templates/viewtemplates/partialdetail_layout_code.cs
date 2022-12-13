using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.scaffolding.EntityDefinition;

namespace t4mvc.scaffolding.templates.viewtemplates
{
    public partial class partialdetail_layout
    {
        public partialdetail_layout(string areaName, Entity entity)
        {
            this.AreaName = areaName;
            this.Entity = entity;
        }
        public string AreaName { get; set; }
        public Entity Entity { get; set; }

    }
}
