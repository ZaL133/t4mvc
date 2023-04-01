using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using t4mvc.web.core.Annotation;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.Models;

namespace t4mvc.web.core.Rendering
{
    public static partial class t4mvcHtmlHelper
    {

        /// <summary>
        /// Edit / Save button
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="editButtonLink"></param>
        /// <returns></returns>
        public static HtmlString EditSaveButton(this IHtmlHelper helper, string editButtonLink = null, string saveButtonLink = null)
        {
            return Current.EditMode ? SaveButton(saveButtonLink) : EditButton(editButtonLink);
        }
        private static HtmlString SaveButton(string saveButtonLink)
        {
            return new HtmlString("<input type=\"hidden\" name=\"PostSave\" value=\"" + saveButtonLink + "\" /><input type=\"submit\" name=\"FormAction\" value=\"Save\" class=\"btn btn-success\" /> <input type=\"submit\" name=\"FormAction\" value=\"Save and close\" class=\"btn btn-success\" />");
        }
        private static HtmlString EditButton(string editButtonLink)
        {
            if (Current.ReturnUrl != null) { editButtonLink = editButtonLink + "?returnUrl=" + Current.ReturnUrlEncoded; }
            return new HtmlString("<a class=\"btn btn-warning\" id=\"edit-button\" href=\"" + 
                
                
                editButtonLink + "\">Edit</a>");
        }

        public static HtmlString BackCancelButton(this IHtmlHelper helper, string defaultUrl)
        {
            return new HtmlString("<a class=\"btn btn-default\" href=\"" + (string.IsNullOrWhiteSpace(Current.ReturnUrl) ? defaultUrl : Current.ReturnUrl) +
                            "\">" + (Current.EditMode ? "Cancel" : "Back") + "</a>");
        }

        public static HtmlString DisabledCheckbox(this IHtmlHelper helper, string label)
        {
            return new HtmlString($@"<label disabled=""disabled""><input type=""checkbox"" disabled=""disabled"" /> {label}</label>");
        }

        public static HtmlString RecordInfo(this IHtmlHelper helper, Guid? moduser, DateTime? modstamp)
        {
            return modstamp.HasValue ? new HtmlString($@"

                <div class=""dropup col-2"">
                    <a href=""#"" class=""dropdown-toggle"" data-bs-toggle=""dropdown"" aria-expanded=""false"" style=""color:white;"" >
                        <i data-feather=""info""></i>
                    </a>
                    <div class=""dropdown-menu"" style=""width:auto;"">
                        <div class=""audit-card"">
                            <img data-user-thumbnail='{moduser}' />
                            <div class=""card-body"">
                                <div class=""user-info"">
                                    <span class=""card-user_bold""><b>Modified By</b>: <span data-user-fullname='{moduser}'></span></span>
                                    <span class=""card-user_bold""><b>Modified On</b>: {modstamp.Value.ToShortDateString()}</span>
                                </div>
                            </div>
                        </div>
                    </div>
               </div>
            ")
            : null;
        }

        private const string DEFAULTTABNAME = "Default";

        public static HtmlString EmptyPlaceholderFormControl(this IHtmlHelper helper)
        {
            return new HtmlString(@"<div class=""form-group"" style=""opacity: 0; "">
        <div class=""col-md-12"">
        <input class=""form-control"" />
    </div>
</div>");
        }

        public static HtmlString AsPhoneNumber(this HtmlString control)
        {
            return new HtmlString("<div class=\"input-group phonenumber-input\">" + control + "</div>");
        }

        public static HtmlString AsDollarInput(this HtmlString control)
        {
            return new HtmlString("<div class=\"input-group money-input\"><div class=\"input-group-addon\"><i class=\"fa fa-usd\"></i></div>" + control + "</div>");
        }

        public static HtmlString AsPercentInput(this HtmlString control)
        {
            return new HtmlString(@"<div class=""input-group"">
                                                    <input class=""form-control text-box single-line"" data-val=""true"" data-val-number=""The field cn_net30 must be a number."" disabled=""disabled"" id=""cn_net30"" name=""cn_net30"" type=""text"" value=""0.00"">
                                                    <span class=""input-group-addon""><i class=""fa fa-percent""></i></span>
                                                </div>");
        }

        public static HtmlString EmptyRow(this IHtmlHelper helper)
        {
            return new HtmlString(@"<div class=""row"" style=""opacity: 0; ""><div>" + helper.EmptyPlaceholderFormControl() + "</div></div>");
        }

        /// <summary>
        /// This creates a select2 list
        /// </summary>
        /// <param name="html"></param>
        /// <param name="fieldExpression">The property name</param>
        /// <param name="keyExpression">The initial key</param>
        /// <param name="valueExpression">The initial text</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent Select2EditorFor<TModel, TValue1, TValue2, TValue3>(this IHtmlHelper<TModel> html,
                                                                                     Expression<Func<TModel, TValue1>> fieldExpression,
                                                                                     Expression<Func<TModel, TValue2>> keyExpression,
                                                                                     Expression<Func<TModel, TValue3>> valueExpression)
        {

            var modelMetadata = html.ViewContext.HttpContext.RequestServices.GetRequiredService<IModelExpressionProvider>();
            var memberExpression = fieldExpression.Body as MemberExpression;
            var propertyInfo = memberExpression.Member as PropertyInfo;
            var customAttributes = propertyInfo.GetCustomAttributes();
            var select2Attribute = customAttributes.OfType<Select2>().FirstOrDefault();
            var editableAttribute = customAttributes.OfType<EditableAttribute>().FirstOrDefault();
            var key = modelMetadata.CreateModelExpression(html.ViewData, keyExpression).Model?.ToString();
            
            // for checking if an option is selected, treat nulls and empty guids the same
            if (key == Guid.Empty.ToString()) key = null;
            
            var value = modelMetadata.CreateModelExpression(html.ViewData, valueExpression).Model?.ToString();

            if (select2Attribute == null) throw new InvalidOperationException();

            var selectedOption = string.IsNullOrEmpty(key + value) ? null : $"<option value='{key}' selected='true'>{value}</option>";
            var disabled = (Current.EditMode && (editableAttribute == null || editableAttribute.AllowEdit)) ? null : "disabled";

            if (disabled == "disabled")
            {
                return html.EditorFor(fieldExpression, "Lookup", new { select2Attribute, text = value });
            }
            else
            {
                return new HtmlString($@"<select name='{propertyInfo.Name}' class='select2 t4mvc-select2 form-control' data-t4mvc-api='{select2Attribute.Api}' data-t4mvc-id='{select2Attribute.Id}' data-t4mvc-text='{select2Attribute.Text}' {disabled}>{selectedOption}</select>");
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent t4mvcEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.t4mvcEditorForDispatch(expression, null, null, 10);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent t4mvcEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData)
        {
            return html.t4mvcEditorForDispatch(expression, null, additionalViewData, 10);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent t4mvcEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName)
        {
            return html.t4mvcEditorForDispatch(expression, templateName, null, 10);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent t4mvcEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, object additionalViewData)
        {
            return html.t4mvcEditorForDispatch(expression, templateName, additionalViewData, 10);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        private static IHtmlContent t4mvcEditorForDispatch<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, object additionalViewData, int columnWidth)
        {
            var memberExpression = expression.Body as MemberExpression;
            var propertyInfo = memberExpression.Member as PropertyInfo;
            var customAttributes = propertyInfo.GetCustomAttributes();
            // var select2Attribute    = customAttributes.OfType<Select2>().FirstOrDefault();
            var editableAttribute = customAttributes.OfType<EditableAttribute>().FirstOrDefault();

            // EditMode is true, and either there is no editable attribute, or the editable attribute is true
            if (Current.EditMode && (editableAttribute == null || editableAttribute.AllowEdit))
            {
                return html.EditorFor(expression, templateName, new { htmlAttributes = new { @class = $"form-control" } });
            }
            else // EditMode is false, or the editableAttribute is false
            {

                return html.EditorFor(expression, templateName, new { htmlAttributes = new { @class = $"form-control", @readonly = "readonly" } });
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent ReadOnlyt4mvcEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.ReadOnlyt4mvcEditorForDispatch(expression, null, null, 10);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent ReadOnlyt4mvcEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, int columnWidth)
        {
            return html.ReadOnlyt4mvcEditorForDispatch(expression, null, null, columnWidth);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent ReadOnlyt4mvcEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData)
        {
            return html.ReadOnlyt4mvcEditorForDispatch(expression, null, additionalViewData, 10);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent ReadOnlyt4mvcEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName)
        {
            return html.ReadOnlyt4mvcEditorForDispatch(expression, templateName, null, 10);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent ReadOnlyt4mvcEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, object additionalViewData)
        {
            return html.ReadOnlyt4mvcEditorForDispatch(expression, templateName, additionalViewData, 10);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        private static IHtmlContent ReadOnlyt4mvcEditorForDispatch<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName, object additionalViewData, int columnWidth)
        {
            var memberExpression = expression.Body as MemberExpression;
            var propertyInfo = memberExpression.Member as PropertyInfo;
            var select2Attribute = propertyInfo.GetCustomAttributes().OfType<Select2>().FirstOrDefault();
            Settings.SetReadonlyProperty(propertyInfo.Name);

            if (select2Attribute != null)
            {
                var selectString = string.Format("<select class='select2 gbm-select2 form-control' data-gbm-api='{0}' data-gbm-id='{1}' data-gbm-text='{2}'></select>", select2Attribute.Api, select2Attribute.Id, select2Attribute.Text);
                return new HtmlString(selectString);
            }

            return html.EditorFor(expression, templateName, new
            {
                htmlAttributes = new { @class = $"form-control", @readonly = "readonly" },
                t4mvcControlAttributes = new t4mvcControlAttribute() { Disabled = true }
            });
        }

        public static HtmlString RadioButtonListFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) where TValue : struct, IConvertible
        {
            var modelMetadata = html.ViewContext.HttpContext.RequestServices.GetRequiredService<IModelExpressionProvider>();
            var memberExpression = expression.Body as MemberExpression;
            var propertyInfo = memberExpression.Member as PropertyInfo;
            var value = modelMetadata.CreateModelExpression(html.ViewData, expression).Model;

            var tValueType = typeof(TValue);
            if (tValueType.IsEnum)
            {
                StringBuilder result = new StringBuilder();
                var values = Enum.GetValues(tValueType);
                foreach (var val in values)
                {
                    var description = ((TValue)val).GetEnumDescription();
                    if (val.ToString() == value.ToString())
                    {
                        result.AppendLine($"<label><input type='radio' checked='checked' value='{val}' name='{propertyInfo.Name}' /> {description}</label><br>");
                    }
                    else
                    {
                        result.AppendLine($"<label><input type='radio' value='{val}' name='{propertyInfo.Name}' /> {description}</label><br>");
                    }
                }
                return new HtmlString(result.ToString());
            }
            return new HtmlString(tValueType.Name + " is not an enum");
        }

        public static HtmlString RadioButtonListFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, params SelectListItem[] items)
        {
            var modelMetadata = html.ViewContext.HttpContext.RequestServices.GetRequiredService<IModelExpressionProvider>();
            var memberExpression = expression.Body as MemberExpression;
            var propertyInfo = memberExpression.Member as PropertyInfo;
            var value = modelMetadata.CreateModelExpression(html.ViewData, expression).Model;
            var disabled = Current.EditMode ? "" : "disabled";

            StringBuilder result = new StringBuilder();
            foreach (var val in items)
            {
                if (value != null && val.Value == value.ToString())
                {
                    result.AppendLine($"<label><input type='radio' checked='checked' value='{val.Value}' {disabled} name='{propertyInfo.Name}' /> {val.Text}</label><br>");
                }
                else
                {
                    result.AppendLine($"<label><input type='radio' value='{val.Value}' {disabled} name='{propertyInfo.Name}' /> {val.Text}</label><br>");
                }
            }
            return new HtmlString(result.ToString());
        }

        public static Action<IUrlHelper, SidebarMenuModel> AddCodeGenFunc { get; set; }
        public static SidebarMenuModel GetSidebarMenuModel(this IHtmlHelper helper)
        {
            var url         = Current.UrlHelper;
            var rv          = new SidebarMenuModel() { MenuLinks = new List<SidebarMenuLink>() };


            // To-do fix
            if (AddCodeGenFunc != null)
            {
                AddCodeGenFunc(url, rv);
            }

            return rv;
        }
        public static IHtmlContent SidebarMenu(this IHtmlHelper helper)
        {
            var model   = helper.GetSidebarMenuModel();

            return helper.Partial("~/Views/Partials/SidebarNav.cshtml", model);
        }
    }
}
