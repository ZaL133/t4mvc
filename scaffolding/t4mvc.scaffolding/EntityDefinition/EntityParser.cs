using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.scaffolding.EntityDefinition
{
    public class EntityParser
    {
        public static IEnumerable<Entity> ParseSpecFile(string specFileContents)
        {
            List<Entity> entities = new List<Entity>();
            Entity e = null;

            foreach (var specFileLine in specFileContents.Split('\n'))
            {
                if (specFileLine.Length > 0 && specFileLine[0] == '#') continue;
                if (specFileLine.Length > 1)
                {
                    // Entity starts at the beginning of a line
                    // Fields start with a tab
                    if (!specFileLine.StartsWith("\t"))
                    {
                        var entityParts = specFileLine.Split('|').Select(x => x.Trim()).ToArray();
                        e = new Entity() { Name = entityParts[0] };
                        foreach (var part in entityParts) e.Attributes.Add(part);

                        if (!e.RawData)
                        {
                            e.Fields.Add(new Field() { IsKeyField = true, Name = e.Name.ToSchemaName() + "Id", DataType = "Guid" });
                            e.Fields.Add(new Field() { Name = "CreateUserId", DataType = "Guid", IsNullable = false, IgnoreOnUpdate = true, IsAudit = true });
                            e.Fields.Add(new Field() { Name = "CreateDate", DataType = "DateTime", IsNullable = false, IgnoreOnUpdate = true, IsAudit = true });
                            e.Fields.Add(new Field() { Name = "ModifyUserId", DataType = "Guid", IsNullable = false, IsAudit = true });
                            e.Fields.Add(new Field() { Name = "ModifyDate", DataType = "DateTime", IsNullable = false, IsAudit = true });
                        }

                        var declassify = entityParts.FirstOrDefault(x => x.StartsWith("Declassify"));
                        if (declassify != null)
                        {
                            foreach (var fieldToDeclassify in declassify.Replace("Declassify(", "").TrimEnd(")").Split(',').Select(x => x.Trim()))
                            {
                                var field = e.Fields.Where(x => x.Name == fieldToDeclassify).Single();
                                field.IsAudit = false;
                                field.IgnoreOnUpdate = false;
                            }
                        }

                        entities.Add(e);
                    }
                    else
                    {
                        var fieldParts = specFileLine.TrimStart('\t').Split('|')
                                                     .Select(x => x.Trim())
                                                     .ToArray();

                        string fieldName, dataType;

                        if (fieldParts[0].Contains("("))
                        {
                            fieldName = fieldParts[0].Split('(')[0];
                            dataType = fieldParts[0].Split('(')[1].TrimEnd(')');

                            // If it's an enum, look for the [val1, val2] part and assign it to the field
                            if (dataType.Contains('[') && dataType.StartsWith("enum"))
                            {
                                var enumValues = dataType.Split('[')[1]
                                                         .TrimEnd(']');

                                dataType = dataType.Split('[')[0];
                            }

                        }
                        else
                        {
                            fieldName = fieldParts[0];
                            dataType = "string"; // default
                        }

                        var field = new Field() { DataType = dataType, Attributes = fieldParts.ToList() };

                        field.SetNameAndNullable(fieldName);
                        field.SetExplicitReferences(fieldParts, entities);

                        // IsIndexed
                        if (fieldParts.Contains("IsIndexed")) field.IsIndexed = true;
                        if (fieldParts.Contains("IsSearchable")) field.IsSearchable = true;
                        if (fieldParts.Contains("IsNameField")) field.IsNameField = true;
                        if (fieldParts.Contains("Secure")) field.Secure = true;
                        if (fieldParts.Contains("GridExclude")) field.GridExclude = true;
                        field.SetViewModelAttributes(fieldParts.SingleOrDefault(x => x.StartsWith("ViewModelAttributes")));
                        if (fieldParts.Any(x => x.StartsWith("SearchOperator"))) field.SearchOperator = fieldParts.Single(x => x.StartsWith("SearchOperator")).Split(':')[1];
                        if (fieldParts.Any(x => x.StartsWith("ParentFieldName"))) field.ParentFieldName = fieldParts.Single(x => x.StartsWith("ParentFieldName")).Split(':')[1];
                        if (fieldParts.Where((x, y) => y > 0).Any(x => x.StartsWith("Description"))) field.Description = fieldParts.Where((x, y) => y > 0).Single(x => x.StartsWith("Description")).Split(':')[1].Trim();
                        if (fieldParts.Has("ViewModelType"))
                            field.ViewModelType = fieldParts.GetVal("ViewModelType");
                        if (e.KeyField == null && fieldParts.Contains("KeyField")) field.IsKeyField = true;

                        e.Fields.Add(field);
                    }

                }
            }

            foreach (var blah in entities.Select(x => x.Fields.Where(y => !y.IsKeyField && y.Name.EndsWith("Id") && !y.NoReference)))
            {
                foreach (var field in blah)
                {
                    var entity = entities.SingleOrDefault(x => x.Name.ToSchemaName() == field.Name.TrimEnd("Id"));

                    if (entity != null)
                        field.References = entity;
                }
            }

            PopulateChildReferences(entities);

            LayoutParser.ParseLayoutFile(entities);

            return entities;
        }

        static void PopulateChildReferences(List<Entity> entities)
        {
            foreach (var entity in entities.Where(x => x.Fields.Any(y => y.References != null)))
            {
                foreach (var field in entity.Fields.Where(x => x.References != null))
                {
                    var referencedEntity = entities.Single(x => x == field.References);
                    referencedEntity.ChildReferences.Add(new ChildReferenceEntity(entity,
                                                                                 (field.ParentFieldName ?? entity.PluralName.ToSchemaName()))
                    {
                        Field = field,
                        ProcessFunction = field.ProcessFunction,
                        Prefetch = field.Prefetch,
                        Tabbed = field.ReferenceTabbed,
                        TabText = field.ReferenceTabText
                    });
                }
            }
        }
    }
}
