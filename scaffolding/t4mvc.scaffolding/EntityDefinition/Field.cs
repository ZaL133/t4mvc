using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.scaffolding.EntityDefinition
{
    public class Field
    {
        public string? Name { get; set; }
        public List<string> Attributes { get; set; } = new List<string>();
        public string DataType { get; set; } = "string"; // default
        public string? ViewModelType { get; set; }
        public int Length { get; set; }
        public bool IsNameField { get; set; }
        public bool IsNullable { get; set; }
        public bool IsKeyField { get; set; }
        public bool IsSearchable { get; set; }
        public string SearchOperator { get; set; } = "StartsWith";
        public bool IsIndexed { get; set; }
        public bool IsAudit { get; set; }
        public bool IgnoreOnUpdate { get; set; }
        public bool Secure { get; set; }
        public bool Identity { get { return Attributes.Any(x => x == "Identity"); } }
        /// <summary>
        /// The field to use as text in the dropdown
        /// </summary>
        public string? ReferenceName { get; set; }
        public bool ReferenceTabbed { get; set; }
        public string? ReferenceTabText { get; set; }
        public Entity? References { get; set; }
        public bool NoReference { get { return Attributes.Any(x => x == "NoReference"); } }
        public bool IsNumber { get { return this.DataType == "int" || this.DataType == "int?"; } }
        public string? RenderFunction
        {
            get { return Attributes.Where(x => x.StartsWith("RenderFunction")).FirstOrDefault()?.Split(':')[1]; }
        }
        public string? ProcessFunction
        {
            get { return Attributes.Where(x => x.StartsWith("ProcessFunction")).FirstOrDefault()?.Split(':')[1]; }
        }
        public bool Prefetch
        {
            get { return Attributes.Any(x => x == "Prefetch"); }
        }
        public string? ParentFieldName { get; set; }
        public List<string> ViewModelAttributes
        {
            get;
            set;
        } = new List<string>();
        public bool GridExclude { get; internal set; }
        public string? Description { get; internal set; }

        public void SetNameAndNullable(string name)
        {
            if (name.EndsWith("?"))
            {
                this.IsNullable = true;
                name = name.TrimEnd('?');
            }

            this.Name = name;
        }

        internal void SetExplicitReferences(string[] fieldParts, List<Entity> entities)
        {
            var referencesFieldPart = fieldParts.SingleOrDefault(x => x.Trim().StartsWith("References"));
            if (referencesFieldPart != null)
            {
                var parts = GetFieldParts(referencesFieldPart);
                var referencesEntityName = parts[1].Split('(')[0];
                var entity = entities.SingleOrDefault(x => x.Name.ToSchemaName() == referencesEntityName);
                this.References = entity;

                // Override the field to look up name
                if (parts[1].Contains(":"))
                {
                    this.ReferenceName = parts[1].Split(':')[1];
                }

                // Handle tabbed
                var tabParts = parts.SingleOrDefault(x => x.StartsWith("Tabbed", StringComparison.CurrentCultureIgnoreCase));
                if (tabParts != null)
                {
                    this.ReferenceTabbed = true;
                    if (tabParts.Contains("("))
                    {
                        this.ReferenceTabText = tabParts.Split('(')[1]
                                                        .Trim(')');
                    }
                }
            }
        }

        internal void SetViewModelAttributes(string viewModelAttribute)
        {
            if (viewModelAttribute != null)
            {
                var attributes = viewModelAttribute.Split(new[] { '(', ')' })[1].Split(',').Select(x => x.Trim());
                foreach (var attrib in attributes)
                {
                    ViewModelAttributes.Add(viewModelAttributeDefinitions[attrib]);
                }
            }
            // Don't want people updating database generated fields
            if (Identity)
                ViewModelAttributes.Add("[Editable(false)]");
        }

        private static readonly Dictionary<string, string> viewModelAttributeDefinitions = new Dictionary<string, string>()
        {
            { "TextArea", "[DataType(DataType.MultilineText)]" },
            { "AllowHtml", "[AllowHtml]" },
            { "Wysiwyg", "[UIHint(\"t4mvcHtmlTextArea\")]" },
            { "DiskSize", "[UIHint(\"DiskSize\")]" },
            { "ProcessorSpeed", "[UIHint(\"ProcessorSpeed\")]" },
            { "Email", "[UIHint(\"Email\")]" },
            { "Phone", "[UIHint(\"Phone\")]" },
            { "Website", "[UIHint(\"Website\")]" },
            { "ReadOnly", "[Editable(false)]" }
        };

        public override string ToString()
        {
            return Name?.ToString();
        }

        /// <summary>
        /// Can't just split on space becaues sometimes a field part will include a space.
        /// e.g. Tabbed(Sub Accounts)
        /// Need to return that as one part
        /// </summary>
        /// <param name="fieldBlock"></param>
        /// <returns></returns>
        internal string[] GetFieldParts(string fieldBlock)
        {
            var rv = new List<string>();
            bool inOpeningParen = false;
            StringBuilder sb = new StringBuilder();

            for (var i = 0; i < fieldBlock.Length; i++)
            {
                var c = fieldBlock[i];
                if (inOpeningParen)
                {
                    if (c == ')')
                    {
                        sb.Append(c);
                        inOpeningParen = false;
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    if (c == '(')
                    {
                        sb.Append(c);
                        inOpeningParen = true;
                    }
                    else
                    {
                        if (c == ' ')
                        {
                            rv.Add(sb.ToString());
                            sb = new StringBuilder();
                        }
                        else
                        {
                            sb.Append(c);
                        }
                    }
                }
            }

            rv.Add(sb.ToString());

            return rv.ToArray();
        }
    }
}
