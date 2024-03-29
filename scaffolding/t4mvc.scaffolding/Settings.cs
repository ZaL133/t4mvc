﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.scaffolding.EntityDefinition;

namespace t4mvc.scaffolding
{
    public static class Settings
    {
        public static string RootPath { get; set; }
        public static string ApplicationName { get; set; }
        public static string DbContextName { get; set; }
        public static string ApplicationUserTypeName => $"{ApplicationName}User";
        public static string ApplicationRoleTypeName => $"{ApplicationName}Role";
        public static List<Entity> Entities { get; set; }
        public static Dictionary<string, Area> AreaDictionary = null;


        public static string CreateAndMapPath(string folder)
        {
            var p = Path.Combine(RootPath, folder);
            Directory.CreateDirectory(p);
            return p;
        }
    }
}
