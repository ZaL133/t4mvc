using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.scaffolding.EntityDefinition
{
    internal static class EntityExtensions
    {
        public static string? ToSchemaName(this string src)
        {
            return src == null ? null : src.Trim().Replace(" ", "");
        }

        public static string? ToCamelCase(this string src)
        {
            if (src == null) return null;

            return src.Substring(0, 1).ToLower() + src.Substring(1);
        }

        public static string? ToLowerCase(this string src)
        {
            return src?.ToLower();
        }

        public static string TrimEnd(this string src, string end)
        {
            if (src.EndsWith(end))
            {
                return src.Remove(src.Length - end.Length, end.Length);
            }
            else return src;
        }

        public static bool Has(this string[] src, string valName)
        {
            return src.Any(x => x.StartsWith(valName));
        }

        public static string GetVal(this string[] src, string valName)
        {
            return src.Single(x => x.StartsWith(valName))
                      .Split(':')[1]
                      .Trim();
        }
    }
}
