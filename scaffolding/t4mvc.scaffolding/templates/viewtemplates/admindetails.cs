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
    using t4mvc.scaffolding.EntityDefinition;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class admindetails : admindetailsBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 7 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 var schemaName   = this.Entity.Name.ToSchemaName(); 
            
            #line default
            #line hidden
            
            #line 7 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"

var hasNotes        = this.Entity.HasNotes;
var areaPrefix      =  this.Area == null ? null : "~/Areas/" + this.Area;

            
            #line default
            #line hidden
            this.Write("@model ");
            
            #line 11 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(schemaName));
            
            #line default
            #line hidden
            this.Write("ViewModel\r\n@{\r\n    ViewBag.Title = \"");
            
            #line 13 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Description));
            
            #line default
            #line hidden
            this.Write(" Details\";");
            
            #line 13 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 if (this.Entity.NameField != null) { 
            
            #line default
            #line hidden
            this.Write("\tViewBag.HtmlTitle = Settings.Icon.GetIcon26(\"");
            
            #line 14 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Icon));
            
            #line default
            #line hidden
            this.Write("\") + \" ");
            
            #line 14 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name));
            
            #line default
            #line hidden
            this.Write(" / \" + Model.");
            
            #line 14 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.NameField.Name.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write(";");
            
            #line 14 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("\tViewBag.HtmlTitle = Settings.Icon.GetIcon26(\"");
            
            #line 15 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Icon));
            
            #line default
            #line hidden
            this.Write("\") + \" ");
            
            #line 15 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name));
            
            #line default
            #line hidden
            this.Write("\";");
            
            #line 15 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 } 
            
            #line default
            #line hidden
            this.Write("}\r\n@section css {\r\n    <link rel=\"stylesheet\" href=\"/api/css/");
            
            #line 18 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write(".details\" />\r\n}\r\n\r\n@using (Html.BeginForm())\r\n{\r\n    <div class=\"row\">\r\n\r\n       " +
                    " <!-- Column 1 -->\r\n        <div class=\"");
            
            #line 26 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((hasNotes) ? "col-xl-8 col-md-8 col-sm-12 col-12" : "col-xl-12 col-md-12 col-sm-12 col-12"));
            
            #line default
            #line hidden
            this.Write("\">\r\n\r\n            @Html.AntiForgeryToken()\r\n            @Html.HiddenFor(x => x.");
            
            #line 29 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.KeyField.Name));
            
            #line default
            #line hidden
            this.Write(@")

            <!-- Details -->
            <div class=""layout-spacing col-xl-12 col-md-12 col-sm-12 col-12"">
                <div class=""statbox widget box box-shadow"">
                    <div class=""widget-content widget-content-area border-top-tab"">

                        <!-- Tabs -->
                        <ul class=""nav nav-tabs"" id=""borderTop"" role=""tablist"">

                            <!-- Details -->
                            <li class=""nav-item"">
                                <a class=""nav-link active"" id=""border-top-details-tab"" data-bs-toggle=""tab"" href=""#border-top-details"" role=""tab"" aria-controls=""border-top-details"" aria-selected=""true"">
                                    Details
                                </a>
                            </li>
");
            
            #line 45 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 foreach(var tab in tabs) { var sn = tab.Name.ToSchemaName(); 
            
            #line default
            #line hidden
            this.Write("                            <!-- ");
            
            #line 46 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((tab.TabText ?? tab.ParentFieldName)));
            
            #line default
            #line hidden
            this.Write(" -->\r\n                            <li class=\"nav-item\">\r\n                        " +
                    "        <a class=\"nav-link\" id=\"border-top-");
            
            #line 48 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sn));
            
            #line default
            #line hidden
            this.Write("-tab\" data-bs-toggle=\"tab\" href=\"#border-top-");
            
            #line 48 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sn));
            
            #line default
            #line hidden
            this.Write("\" role=\"tab\" aria-controls=\"border-top-");
            
            #line 48 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sn));
            
            #line default
            #line hidden
            this.Write("\" aria-selected=\"false\">\r\n                                    ");
            
            #line 49 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((tab.TabText ?? tab.ParentFieldName)));
            
            #line default
            #line hidden
            this.Write("\r\n                                </a>\r\n                            </li>");
            
            #line 51 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"                        </ul>

                        <!-- Tab Content -->
                        <div class=""tab-content"">

                            <!-- Details -->
                            <div id=""border-top-details"" class=""tab-pane fade show active"" role=""tabpanel"" aria-labelledby=""border-top-details-tab"">
                                <partial name=""");
            
            #line 59 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(areaPrefix));
            
            #line default
            #line hidden
            this.Write("/Views/");
            
            #line 59 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write("/_DetailsPartial.cshtml\" model=\"Model\" fallback-name=\"");
            
            #line 59 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(areaPrefix));
            
            #line default
            #line hidden
            this.Write("/Views/");
            
            #line 59 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write("/CodeGen/_DetailsPartial.cshtml\" />\r\n                            </div>\r\n\r\n");
            
            #line 62 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 foreach(var tab in tabs) { var sn = tab.Name.ToSchemaName(); 
            
            #line default
            #line hidden
            this.Write("                            <!-- ");
            
            #line 63 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((tab.TabText ?? tab.ParentFieldName)));
            
            #line default
            #line hidden
            this.Write(" -->\r\n                            <div id=\"border-top-");
            
            #line 64 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sn));
            
            #line default
            #line hidden
            this.Write("\" class=\"tab-pane fade show\" role=\"tabpanel\" aria-labelledby=\"border-top-");
            
            #line 64 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sn));
            
            #line default
            #line hidden
            this.Write("-tab\">\r\n                                <partial name=\"");
            
            #line 65 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(areaPrefix));
            
            #line default
            #line hidden
            this.Write("/Views/");
            
            #line 65 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sn));
            
            #line default
            #line hidden
            this.Write("/_TablePartial.cshtml\" model=\"Model.");
            
            #line 65 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(tab.ParentFieldName));
            
            #line default
            #line hidden
            this.Write("\" fallback-name=\"");
            
            #line 65 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(areaPrefix));
            
            #line default
            #line hidden
            this.Write("/Views/");
            
            #line 65 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(sn));
            
            #line default
            #line hidden
            this.Write("/CodeGen/_TablePartial.cshtml\" />\r\n                            </div>");
            
            #line 66 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 } 
            
            #line default
            #line hidden
            this.Write("                        </div>\r\n                    </div>\r\n                </div" +
                    ">\r\n            </div>\r\n\r\n        </div>\r\n");
            
            #line 73 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 if (hasNotes) { 
            
            #line default
            #line hidden
            this.Write(@"
        <!-- Column 2 -->
        @if (Model.ContactId != default(Guid))
        {
        <div class=""col-xl-4 col-md-4 col-sm-12 col-12"">
            <!-- Notes -->
            <partial name=""~/Views/Partials/NotesHtml.cshtml"" model=""Model.Notes"" />
        </div> 
        }
");
            
            #line 83 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    </div>\r\n\r\n    <!-- Save buttons -->\r\n    <div class=\"account-settings-footer\"" +
                    ">\r\n        <div class=\"as-footer-container\">\r\n            ");
            
            #line 89 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 if (!this.Entity.RawData) { 
            
            #line default
            #line hidden
            this.Write("@Html.RecordInfo(Model?.ModifyUserId, Model?.ModifyDate)\r\n    ");
            
            #line 90 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 } 
            
            #line default
            #line hidden
            this.Write("        <div class=\"col-10\">\r\n                ");
            
            #line 91 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 if (!this.Entity.RawData) { 
            
            #line default
            #line hidden
            this.Write("@Html.EditSaveButton(Url.Action(\"Edit\", new { id = Model.");
            
            #line 91 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(schemaName));
            
            #line default
            #line hidden
            this.Write("Id}),\r\n                                     Url.Action(\"Details\", new { id = Mode" +
                    "l.");
            
            #line 92 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(schemaName));
            
            #line default
            #line hidden
            this.Write("Id }))\r\n    ");
            
            #line 93 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 } 
            
            #line default
            #line hidden
            this.Write("            @Html.BackCancelButton(Current.ReturnUrl ?? Url.Action(\"Index\"))\r\n   " +
                    "     </div>\r\n    </div>\r\n</div>\r\n}\r\n\r\n");
            
            #line 99 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 if (hasNotes) { 
            
            #line default
            #line hidden
            this.Write("@section scripts {\r\n    <partial name=\"~/Views/Partials/NotesJs.cshtml\" model=\"ne" +
                    "w t4mvc.web.core.Models.NoteDefinition() { Id = Model.");
            
            #line 101 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.KeyField.Name));
            
            #line default
            #line hidden
            this.Write(", KeyField = nameof(Model.");
            
            #line 101 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.KeyField.Name));
            
            #line default
            #line hidden
            this.Write(") };\" />\r\n    <script src=\"/api/js/");
            
            #line 102 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Entity.Name.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write(".details\"></script>\r\n} ");
            
            #line 103 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\admindetails.tt"
 } 
            
            #line default
            #line hidden
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
    public class admindetailsBase
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
