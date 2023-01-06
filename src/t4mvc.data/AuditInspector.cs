using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.data
{
    public static class AuditInspector
    {
        public static IEnumerable<string> GetAuditRecord<T>(this T oldRecord, T newRecord, IEnumerable<string> fieldsToIgnore)
        {
            var changedValues = new List<ChangeValue>();
            var allProperties = oldRecord.GetType().GetProperties(System.Reflection.BindingFlags.Instance | BindingFlags.Public)
                                                   // Ignore anything specified in the fieldsToIgnore list
                                                   // This will contain read-only properties as well as the create/modify user/date
                                                   .Where(x => !fieldsToIgnore.Contains(x.Name));

            foreach (var prop in allProperties) 
            {
                var oldValue = prop.GetValue(oldRecord);
                var newValue = prop.GetValue(newRecord);

                if (oldValue != newValue)
                {
                    changedValues.Add(new ChangeValue
                    {
                        OldValue = oldValue?.ToString(),
                        NewValue = newValue?.ToString()
                    });
                }
            }

            return null;
        }
    }

    public class ChangeValue
    {
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
