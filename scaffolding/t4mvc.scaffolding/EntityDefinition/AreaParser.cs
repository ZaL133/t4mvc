using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.scaffolding
{
    internal class AreaParser
    {
        public static IEnumerable<Area> ParseAreas()
        {
            var lines = File.ReadAllLines("area.spec");
            foreach (var line in lines)
            {
                var parts = line.Split('|').Select(x => x.Trim()).ToArray();
                yield return new Area
                {
                    Name        = parts[0],
                    Attributes  = parts.Skip(1).ToArray()
                };
            }
        }

        internal static Dictionary<string, Area> GetAreaSpect()
        {
            return ParseAreas()
                        .ToDictionary(x => x.Name);
        }
    }
}
