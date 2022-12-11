using AutoMapper.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using t4mvc.web.core.Infrastructure;

namespace t4mvc.web.core
{
    public static class Utils
    {
        /// <summary>
        /// Simple extension method around Mapper.Map<T>(src)
        /// </summary>
        /// <typeparam name="TTo">The type to map to</typeparam>
        /// <param name="src">The object to map from</param>
        /// <returns></returns>
        public static TTo Map<TTo>(this object src)
        {
            return Current.Mapper.Map<TTo>(src);
        }

        public static IEnumerable<TTo> MapAll<TFrom, TTo>(this IEnumerable<TFrom> src)
        {
            return src.Select(x => x.Map<TTo>());
        }

        public static IEnumerable<string> GetDestinationProperties<T1, T2>(this T1 src)
        {
            var typeMap = Current.Mapper.ConfigurationProvider.Internal().FindTypeMapFor<T1, T2>();
            return typeMap.PropertyMaps.Select(x => x.DestinationName);
        }

        /// <summary>
        /// Finds all field names which has an [Editable(false)] attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetReadOnlyProperties<T>(this T src)
        {
            var properties = src.GetType().GetProperties(System.Reflection.BindingFlags.Instance | BindingFlags.Public);
            return properties.Where((x) =>
            {
                var customAttribute = x.GetCustomAttributes().OfType<EditableAttribute>().FirstOrDefault();

                // To be read-only, there MUST be a custom attribute and it MUST be false
                return customAttribute != null && customAttribute.AllowEdit == false;
            })
            .Select(x => x.Name);
        }

        // Compares a view model (T1) with the GBM.Core type (T2) to get a list of any properties
        // that exist on T2, but don't exist on T1. Those should be ignored when you save, since they aren't present on the ViewModel
        // and thus will always be null
        public static IEnumerable<string> GetUnmappedFields<T1, T2>(this T1 src)
        {
            List<string> rv             = new List<string>();
            var mappingConfiguration    = Current.Mapper.ConfigurationProvider;
            var allTypeMaps             = mappingConfiguration.Internal().GetAllTypeMaps();
            var srcType                 = allTypeMaps.Single(x => x.SourceType == typeof(T1));

            var viewModelProperties     = srcType.PropertyMaps.Select(x => x.DestinationName)
                                                              .ToList();
            var t2Properties            = typeof(T2).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(x => x.Name);

            return t2Properties.Where(x => !viewModelProperties.Contains(x));
        }
    }
}
