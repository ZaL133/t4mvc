using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.scaffolding.EntityDefinition
{
    public class LayoutParser
    {
        private static readonly string[] LAYOUTSPLITTER = new string[] { "\n\n", "\r\r", "\n\r\n\r", "\r\n\r\n" };
        private static readonly char[] NEWLINESPLITTER = new char[] { '\n', '\r' };
        private static readonly char[] PIPESPLITTER = new char[] { '|' };

        public static void ParseLayoutFile(List<Entity> entities)
        {
            var layoutFileSpec      = "layout.spec";
            
            var layoutContents      = File.ReadAllText(layoutFileSpec);
            var layoutSpecSections  = layoutContents.Split(LAYOUTSPLITTER, StringSplitOptions.RemoveEmptyEntries);

            // Just an easier way to look up the entity
            var entityDictionary = entities.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            foreach (var layoutSpecSection in layoutSpecSections)
            {
                var layoutSpecSectionLines = layoutSpecSection.Split(NEWLINESPLITTER, StringSplitOptions.RemoveEmptyEntries).ToList();

                var layout      = new Layout();
                Section section = null;

                // Assign the layout to the correct entity
                // This should fail if it can't be found.
                var entity = entityDictionary[layoutSpecSectionLines[0]];
                entity.Layout = layout;

                // Create a dictionary of fields to look up
                var fieldDictionary = entity.Fields.ToDictionary(x => x.Name, StringComparer.CurrentCultureIgnoreCase);

                for (var i = 1; i < layoutSpecSectionLines.Count; i++)
                {
                    var line = layoutSpecSectionLines[i];

                    if (line.StartsWith("#"))
                    {
                        var sectionParts = line.Split(PIPESPLITTER, StringSplitOptions.RemoveEmptyEntries)
                                               .Select(x => x.Trim())
                                               .ToArray();
                        section = new Section() { Title = sectionParts[0].Replace("#", "").Trim() };
                        if (sectionParts.Any(x => x.StartsWith("Icon")))
                        {
                            section.Icon = sectionParts.Single(x => x.StartsWith("Icon"))
                                                       .Split(':')[1];
                        }
                        layout.Sections.Add(section);
                        continue;
                    }

                    var row = new Row();

                    // If we're in a section, add the row there.
                    // Otherwise add it to the layouts
                    // Once you enter a section, you can only end it by starting a new section. Anything else goes at the beginning
                    if (section == null)
                        layout.Rows.Add(row);
                    else
                        section.Rows.Add(row);

                    var fields = line.Split('|').Select(x => x.Trim()).ToList();
                    for (var fieldNumber = 0; fieldNumber < fields.Count; fieldNumber++)
                    {
                        var field = fields[fieldNumber];
                        var fieldParts = field.Split(':');
                        var fieldName = fieldParts[0];
                        var layoutField = new LayoutField(fieldDictionary[fieldName]);

                        if (fieldParts.Length > 1)
                        {
                            var columnWidth = int.Parse(fieldParts[1]);
                            layoutField.Columns = columnWidth;
                        }


                        row.Fields.Add(layoutField);
                    }

                    // Calculate the rest of the field widths
                    int remainingFieldCount = row.Fields.Count(x => x.Columns == 0);
                    int usedLength = row.Fields.Sum(x => x.Columns);
                    int remainingColumns = 12 - usedLength;
                    int amountPerColumn = remainingColumns / remainingFieldCount;

                    foreach (var layoutField in row.Fields.Where(x => x.Columns == 0))
                    {
                        layoutField.Columns = amountPerColumn;
                    }
                }
                Console.Write(layoutSpecSection);
            }
        }
    }
}
