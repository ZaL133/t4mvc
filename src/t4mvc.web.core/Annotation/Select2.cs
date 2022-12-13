using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.web.core.Annotation
{
    public class Select2 : System.Attribute
    {
        public string Api { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public string Groups { get; set; }
        public bool Prefetch { get; set; }
        public string ProcessFunction { get; set; }
        /// <summary>
        /// The entity to look up. This can be used to generate a link to the object when not looking up
        /// </summary>
        public string Entity { get; set; }
        /// <summary>
        /// The area of the entity to look up
        /// </summary>
        public string Area { get; set; }

        public Select2(string api, string id = null, string text = null, string entity = null, string groups = null, bool prefetch = false, string processFunction = null, string area = null)
        {
            this.Api                = api;
            this.Id                 = id;
            this.Text               = text;
            this.Entity             = entity;
            this.Groups             = groups;
            this.Prefetch           = prefetch;
            this.ProcessFunction    = processFunction;
            this.Area               = area;
        }
    }
}
