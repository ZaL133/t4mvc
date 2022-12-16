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
    
    #line 1 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class partialdetail_layout : partialdetail_layoutBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 5 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
 var schemaName = this.Entity.Name.ToSchemaName(); 
            
            #line default
            #line hidden
            this.Write("@model ");
            
            #line 6 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(schemaName));
            
            #line default
            #line hidden
            this.Write("ViewModel\r\n\r\n@Html.ValidationSummary(true, \"\", new { @class = \"text-danger\" })\r\n\r" +
                    "\n");
            
            #line 10 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
 foreach(var row in this.Entity.Layout.Rows)
    {

            
            #line default
            #line hidden
            this.Write("                    <div class=\"form-row\">");
            
            #line 13 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"

        foreach(var layoutField in row.Fields)
        {
            
            
            #line default
            #line hidden
            this.Write("                        <div class=\"form-group col-");
            
            #line 17 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(layoutField.Columns));
            
            #line default
            #line hidden
            this.Write("\">\r\n            ");
            
            #line 18 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(new fieldpartial(layoutField.Field, layoutField.Columns).TransformText()));
            
            #line default
            #line hidden
            this.Write("\r\n                        </div>\r\n            ");
            
            #line 20 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"

        }

            
            #line default
            #line hidden
            this.Write("                    </div>");
            
            #line 23 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"

    }

            
            #line default
            #line hidden
            this.Write("\r\n<div class=\"sections-accordion accordion\" id=\"sections-accordion\">\r\n");
            
            #line 28 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
 int i = 0; foreach(var section in this.Entity.Layout.Sections)
    {

            
            #line default
            #line hidden
            this.Write("    <div class=\"card accordion-item\">\r\n        <div class=\"card-header accordion-" +
                    "header\" id=\"accordion-label-");
            
            #line 32 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(i));
            
            #line default
            #line hidden
            this.Write("\">\r\n            <section class=\"mb-0 mt-0\">\r\n                <div role=\"menu\" ");
            
            #line 34 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
 if (!section.Expanded) { 
            
            #line default
            #line hidden
            this.Write("class=\"collapsed\" aria-expanded=\"false\"");
            
            #line 34 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
 }
            
            #line default
            #line hidden
            this.Write(" data-bs-toggle=\"collapse\" data-bs-target=\"#accordion-");
            
            #line 34 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(i));
            
            #line default
            #line hidden
            this.Write("\" aria-controls=\"accordion-");
            
            #line 34 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(i));
            
            #line default
            #line hidden
            this.Write("\">\r\n                    ");
            
            #line 35 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
 if (section.Icon != null) { 
            
            #line default
            #line hidden
            this.Write("<i data-feather=\"");
            
            #line 35 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(section.Icon));
            
            #line default
            #line hidden
            this.Write("\"></i>");
            
            #line 35 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
 } 
            
            #line default
            #line hidden
            this.Write("                    ");
            
            #line 36 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(section.Title));
            
            #line default
            #line hidden
            this.Write("\r\n                    <div class=\"icons\"><i data-feather=\"chevron-down\"></i></div" +
                    ">\r\n                </div>\r\n            </section>\r\n        </div>\r\n        <div " +
                    "id=\"accordion-");
            
            #line 41 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(i));
            
            #line default
            #line hidden
            this.Write("\" class=\"collapse accordion-collapse");
            
            #line 41 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
 if (section.Expanded) { 
            
            #line default
            #line hidden
            this.Write(" show");
            
            #line 41 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\" aria-labelledby=\"accordion-label-");
            
            #line 41 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(i));
            
            #line default
            #line hidden
            this.Write("\">\r\n            <div class=\"card-body\">\r\n");
            
            #line 43 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"

    foreach(var row in section.Rows) {

            
            #line default
            #line hidden
            this.Write("                <div class=\"form-row\">");
            
            #line 46 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"

        foreach(var layoutField in row.Fields)
        {
            
            
            #line default
            #line hidden
            this.Write("                        <div class=\"form-group col-");
            
            #line 50 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(layoutField.Columns));
            
            #line default
            #line hidden
            this.Write("\">\r\n            ");
            
            #line 51 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(new fieldpartial(layoutField.Field, layoutField.Columns).TransformText()));
            
            #line default
            #line hidden
            this.Write("\r\n                        </div>\r\n");
            
            #line 53 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"

        }

            
            #line default
            #line hidden
            this.Write("                </div>");
            
            #line 56 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"

    }

            
            #line default
            #line hidden
            this.Write("            </div>\r\n        </div>\r\n    </div>\r\n");
            
            #line 62 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\viewtemplates\partialdetail_layout.tt"
 i++;
    }

            
            #line default
            #line hidden
            this.Write("</div>\r\n");
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
    public class partialdetail_layoutBase
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
