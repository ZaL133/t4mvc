using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.scaffolding.EntityDefinition
{
    public class ChildReferenceEntity : Entity
    {
        public Field? Field { get; set; }
        public string? ParentFieldName { get; set; }
        public bool? Prefetch { get; set; }
        public string? ProcessFunction { get; set; }
        public bool? Tabbed { get; set; }
        public string? TabText { get; set; }
        public ChildReferenceEntity(Entity entity, string parentFieldName)
        {
            this.Name               = entity.Name;
            this.Attributes         = entity.Attributes;
            this.ChildReferences    = entity.ChildReferences;
            this.ParentFieldName    = parentFieldName;

            this.Fields.AddRange(entity.Fields);
        }
    }
}
