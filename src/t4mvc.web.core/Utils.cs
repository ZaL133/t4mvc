using AutoMapper.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.Models;

namespace t4mvc.web.core
{
    public static class Utils
    {
        public static Regex HtmlRegex = new Regex("<[^>]*>");
        public static Regex TrimPrecision = new Regex(@"(?<=\d\.\d\d)(\d\d)");

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

        public static string GetEnumDescription<T>(this T value) where T : struct, IConvertible
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }


        /// <summary>
        /// Sorts a IQueryable<T> for use with a datatables.net sort parameter
        /// </summary>
        public static IQueryable<T> Sort<T>(this IQueryable<T> src, DataTablesRequestBase dtParams)
        {
            if (dtParams.Order == null || dtParams.Order.Length == 0)
                return src;

            IOrderedQueryable<T> result = null;
            foreach (var sort in dtParams.Order)
            {
                // The first pass this is null, meaning it's the first sort. Use OrderBy
                if (result == null)
                {
                    result = src.OrderBy(dtParams.Columns[sort.Column].Data, sort.Dir == "asc");
                }
                else // Subsequent passes, use ThenBy to sort by multiple conditions
                {
                    result = result.ThenBy(dtParams.Columns[sort.Column].Data, sort.Dir == "asc");
                }
            }
            return result;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> src, DataTablesRequestBase dtParams)
        {
            var where = src;
            foreach (var filter in dtParams.Columns.Where(x => !string.IsNullOrWhiteSpace(x.Search.Value) && x.Search.Value != "null"))
            {
                where = where.Where($"{filter.Data} == @0", filter.Search.Value);
            }

            return where;
        }

        // sort by text https://stackoverflow.com/a/40572006/972250
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string key, bool ascending = true)
        {
            var expression = (dynamic)CreateExpression(typeof(TSource), key);

            return ascending
                ? Queryable.OrderBy(query, expression)
                : Queryable.OrderByDescending(query, expression);
        }

        public static IOrderedQueryable<TSource> ThenBy<TSource>(this IOrderedQueryable<TSource> query, string key, bool ascending = true)
        {
            var expression = (dynamic)CreateExpression(typeof(TSource), key);

            return ascending
                ? Queryable.ThenBy(query, expression)
                : Queryable.ThenByDescending(query, expression);
        }

        private static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");
            Expression body = param;
            body = Expression.PropertyOrField(body, propertyName);

            return Expression.Lambda(body, param);
        }

        public static string FormatNullableBool(bool? src, string t, string f)
        {
            return src.HasValue && src.Value ? t : f;
        }

        public static string YesNo(bool? src)
        {
            return FormatNullableBool(src, Consts.YES, Consts.NO);
        }

        public static string YesNo(int? src)
        {
            return YesNo(src == 1);
        }

        public static string FormatDataTablesValue(string input, string render)
        {

            switch (render)
            {
                case "formatYesNoFromByteOrInt":
                    return YesNo(render == "1");
                case "formatDate":
                    if (string.IsNullOrWhiteSpace(input)) return null;
                    return DateTime.Parse(input).ToShortDateString();
                case "formatMoney":
                    if (string.IsNullOrWhiteSpace(input)) return null;
                    var tmp = decimal.Parse(input);
                    return tmp.ToString("C");
                default:
                    if (string.IsNullOrWhiteSpace(input)) return input;
                    input = Utils.HtmlRegex.Replace(input, "");
                    input = Utils.TrimPrecision.Replace(input, "");
                    input = input.Trim();
                    break;
            }

            return input;
        }
    }
}
