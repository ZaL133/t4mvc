﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace t4mvc.scaffolding.templates.viewtemplates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class adminindexview : adminindexviewBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 5 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
 var schemaName = this.Entity.Name.ToSchemaName(); 
            
            #line default
            #line hidden
            this.Write("@{\r\n    ViewBag.Title = \"");
            
            #line 7 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.PluralFullName ?? (this.Entity.Description + "s")));
            
            #line default
            #line hidden
            this.Write("\";\r\n\tViewBag.HtmlTitle = Settings.Icon.GetIcon26(\"");
            
            #line 8 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Icon));
            
            #line default
            #line hidden
            this.Write("\") + \" ");
            
            #line 8 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.PluralName ?? (this.Entity.Description + "s")));
            
            #line default
            #line hidden
            this.Write(@""";
}

<div class=""widget-content widget-content-area br-6"">
<!-- Create New -->
<div style=""float:left; margin-top: 8px; margin-right: 16px;"">
    @Html.ActionLink(""Create"", ""Create"", new { }, new { @class = ""btn btn-default"" })
</div>

<table class=""table dataTable"" role=""grid"" id=""");
            
            #line 17 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("-table\">\r\n    <thead>\r\n        <tr>\r\n            <th></th>\r\n");
            
            #line 21 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"

foreach(var field in this.Entity.Fields.Where(x => !x.IsKeyField && !x.IsAudit && !x.Secure && !x.GridExclude)) { 
            
            #line default
            #line hidden
            this.Write("            <th>");
            
            #line 23 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.Name));
            
            #line default
            #line hidden
            this.Write("</th>\r\n");
            
            #line 24 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        </tr>\r\n    </thead>\r\n</table>\r\n</div>\r\n\r\n@section scripts {\r\n    <script>" +
                    "\r\n        $(function () {\r\n\t\t\tvar detailsUrl = \"");
            
            #line 33 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((this.Entity.Area == null ? "" : "/" + this.Entity.Area)));
            
            #line default
            #line hidden
            this.Write("/");
            
            #line 33 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToSchemaName().ToLowerCase()));
            
            #line default
            #line hidden
            this.Write("/details/\";\r\n            var dTable = $(\"#");
            
            #line 34 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("-table\").DataTable({\r\n                \"serverSide\": true,\r\n                \"order" +
                    "ing\": true,\r\n\t\t\t\tstateSave: true,\r\n                dom: ");
            
            #line 38 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName));
            
            #line default
            #line hidden
            this.Write(".excelButtonDom,\r\n                \"ajax\": {\r\n                    \"url\": \"/api/get" +
                    "");
            
            #line 40 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("s?cacheKey=\" + new Date().getTime()\r\n                },\r\n                columns:" +
                    " [\r\n                    { data: \"");
            
            #line 43 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.KeyField.Name));
            
            #line default
            #line hidden
            this.Write("\" },\r\n");
            
            #line 44 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"

foreach(var field in this.Entity.Fields.Where(x => !x.IsKeyField && !x.IsAudit && !x.Secure && !x.GridExclude)) { 
            
            #line default
            #line hidden
            this.Write("                    { data: \"");
            
            #line 45 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.References == null ? field.Name.ToSchemaName() : field.Name.ToSchemaName() + field.References.NameField.Name.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write("\" },\r\n");
            
            #line 46 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"                ],
                columnDefs: [
                    {
                        targets: 0, render: function (a) {
                            return '<a href=""' + detailsUrl + a + '"">@Settings.Icon.DetailsIcon</a>';
                        }
                    },
");
            
            #line 54 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"

foreach(var field in this.Entity.Fields.Where(x => !x.IsKeyField && !x.IsAudit && !x.Secure && !x.GridExclude).Select((field, ix) => new { field, ix }).Where(x => x.field.RenderFunction != null)) { 
            
            #line default
            #line hidden
            this.Write("                    { targets: ");
            
            #line 55 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.ix + 1));
            
            #line default
            #line hidden
            this.Write(", render: ");
            
            #line 55 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(field.field.RenderFunction));
            
            #line default
            #line hidden
            this.Write(" },\r\n");
            
            #line 56 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
 } 
            
            #line default
            #line hidden
            this.Write("                ],\r\n                order: [\r\n                    [1, \"asc\"]\r\n   " +
                    "             ],\r\n                initComplete: function (settings, json) {\r\n    " +
                    "                feather.replace();\r\n                }\r\n            });\r\n\r\n      " +
                    "      $(\"#");
            
            #line 66 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("-table tbody\").on(\"dblclick\", \"tr\", function () {\r\n                var data = dTa" +
                    "ble.row(this).data();\r\n                window.location.href = detailsUrl + data." +
                    "");
            
            #line 68 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.KeyField.Name));
            
            #line default
            #line hidden
            this.Write(";\r\n            });\r\n\r\n            key(\"esc\", ");
            
            #line 71 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName));
            
            #line default
            #line hidden
            this.Write(@".navigateUpOneLevel);
            key(""ctrl+enter"", function () {
                $(dTable.context[0].nTable).find(""tbody tr:first a"")[0].click();
            });

            $(""#export-excel"").click(function () {
                var params = dTable.ajax.params();
                var urlParams = $.param(params);

                // The last data table parameters get cached on the server and so they don't need to be sent here
                window.open(""/api/export/");
            
            #line 81 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToLowerCase()));
            
            #line default
            #line hidden
            this.Write("\");\r\n            });\r\n\r\n        })\r\n    </script>\r\n    <script src=\"/api/js/");
            
            #line 86 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\adminindexview.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write(".index\"></script>\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class adminindexviewBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}