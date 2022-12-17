using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.scaffolding
{
    public class Area
    {
        public string Name { get; set; }
        public string[] Attributes { get; set; }
        public string Security
        {
            get
            {
                return GetParamEnclosed(Attributes.SingleOrDefault(x => x.StartsWith("Security")));
            }
        }

        private string GetParamEnclosed(string val)
        {
            if (val == null || !val.Contains('(')) return val;
            return val.Split('(', ')')[1];
        }
    }
}
