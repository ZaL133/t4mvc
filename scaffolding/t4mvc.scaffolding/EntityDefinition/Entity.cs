using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.scaffolding.EntityDefinition
{
    public class Entity
    {
        public string? Name { get; set; }
        public string SchemaName => Name.ToSchemaName();
        public string CamelCaseName => Name.ToCamelCase();
        public string PluralFullName { get { return Attributes.Where(x => x.StartsWith("Plural:")).Select(x => x.Split(':')[1]).SingleOrDefault() ?? (Description + "s" ?? Name + "s"); } }
        public string? PluralName { get { return PluralFullName?.ToSchemaName(); } }
        public string PluralCamelCaseName => PluralName.ToCamelCase();
        public string ServiceInterfaceTypeName => $"I{Name}Service";
        public string ServiceInterfaceInstanceName => $"{Name.ToCamelCase()}Service";

        public string DetailsUrl => $"{(Area == null ? "" : "/" + Area)}/{SchemaName.ToLowerCase()}/details/";

        public IList<string> Attributes { get; set; } = new List<string>();
        public List<Field> Fields { get; } = new List<Field>();
        public IEnumerable<Field> SearchableFields { get { return Fields.Where(x => x.IsSearchable); } }
        public bool HasIntSearchableFields { get { return SearchableFields.Any(x => x.DataType == "int" || x.DataType == "int?"); } }
        public bool DontScaffold { get { return Attributes.Contains("DontScaffold"); } }
        public bool HasNotes { get { return Attributes.Contains("HasNotes"); } }
        public string Description { get { return Attributes.SingleOrDefault(x => x.StartsWith("Description"))?.Split('(')[1].TrimEnd(")") ?? Name; } }
        public string Area { get { return AreaText.Replace(" ", ""); } }
        public string AreaText { get {
                if (DontScaffold) return "";
                return Attributes.SingleOrDefault(x => x.StartsWith("Area:"))?.Split(':')[1] ?? "Admin"; 
            } }
        public Area AreaDefinition
        {
            get
            {
                if (AreaText != null && Settings.AreaDictionary.ContainsKey(AreaText))
                    return Settings.AreaDictionary[AreaText];
                else
                    return null;
            }
        }
        public string Icon { get { return Attributes.SingleOrDefault(x => x.StartsWith("Icon:"))?.Split(':')[1] ?? "fa-building"; } }
        public string HtmlIcon { get
            {
                // Feather
                if (Icon.StartsWith("feather-")) return $"<i data-feather=\"{Icon.Replace("feather-", "")}\"></i>";

                // Font-awesome
                if (Icon.StartsWith("fa-")) return $"<i class=\"{Icon}\"></i>";

                return $"<i class=\"{Icon}\"></i>";
            } 
        }
        public string Security { get { return Attributes.SingleOrDefault(x => x.StartsWith("Security("))?.Split('(', ')')[1]; } }
        public bool RawData { get { return Attributes.Any(x => x == "RawData"); } }
        /// <summary>
        /// Skip the navigation node
        /// </summary>
        public bool NoNav { get { return Attributes.Any(x => x == "NoNav"); } }
        // Layout
        public Layout? Layout { get; set; }
        // A list of the entities which have a foreign key field for this table
        public List<ChildReferenceEntity> ChildReferences { get; set; } = new List<ChildReferenceEntity>();
        public Field? KeyField
        {
            get
            {
                return this.Fields.SingleOrDefault(x => x.IsKeyField);
            }
        }

        public Field? NameField
        {
            get
            {
                return this.Fields.SingleOrDefault(x => x.IsNameField);
            }
        }

        public IEnumerable<string> GetViewmModelServiceDefinitions()
        {
            return GetViewmModelServiceDefinitionsInternal().Distinct();
        }

        private IEnumerable<string> GetViewmModelServiceDefinitionsInternal()
        {
            if (Name != "User")
                yield return $"I{Name}Service {Name.ToCamelCase()}Service";
            foreach (var field in Fields.Where(x => x.References != null && x.References.Name != "User"))
            {
                yield return $"{field.References.ServiceInterfaceTypeName} {field.References.ServiceInterfaceInstanceName}";
            }
            foreach (var childRef in ChildReferences.Where(x => x.Name != this.Name))
            {
                yield return $"I{childRef.Name}ViewModelService {childRef.Name.ToCamelCase()}ViewModelService";
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
