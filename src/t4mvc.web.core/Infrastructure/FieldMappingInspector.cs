using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.web.core.Infrastructure
{
    public static class FieldMappingInspector<T2>
    {
        /// <summary>
        /// Gets all fields which should not be updated
        /// This can be either a property which exists on the viewmodel but not the model - or has an editable(false) attribute on the viewmodel
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAllReadonlyFields<T1>(T1 src)
        {
            var allDestinationFields    = src.GetDestinationProperties<T1, T2>();
            var fieldsToIgnore          = src.GetReadOnlyProperties().Where(x => allDestinationFields.Contains(x))
                                                                     .ToList();
            fieldsToIgnore.AddRange(src.GetUnmappedFields<T1, T2>());

            return fieldsToIgnore;
        }
    }
}
