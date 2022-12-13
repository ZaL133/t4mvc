using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.scaffolding.EntityDefinition;

namespace t4mvc.scaffolding.templates.viewtemplates
{
    public partial class partialdetail_nolayout
    {
        public partialdetail_nolayout(string areaName, Entity entity)
        {
            this.AreaName   = areaName;
            this.Entity     = entity;
        }
        public string AreaName { get; set; }
        public Entity Entity { get; set; }

    }
}
