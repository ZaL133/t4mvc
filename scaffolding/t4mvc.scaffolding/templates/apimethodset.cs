﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace t4mvc.scaffolding.templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class apimethodset : apimethodsetBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing ");
            
            #line 9 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write(".web.core;\r\nusing ");
            
            #line 10 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write(".web.core.Infrastructure;\r\nusing ");
            
            #line 11 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write(".web.core.Models;\r\nusing ");
            
            #line 12 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write(".web.core.ViewModels;\r\nusing ");
            
            #line 13 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write(".web.core.ViewModelServices;\r\nusing Microsoft.AspNetCore.Mvc;");
            
            #line 14 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"

var constructorParams = string.Join(",", this.Entities.Select(x => "I" + x.Name + "ViewModelService " + x.Name.ToCamelCase() +"ViewModelService"));
            
            #line default
            #line hidden
            this.Write("\r\nnamespace ");
            
            #line 17 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName));
            
            #line default
            #line hidden
            this.Write(".web.Controllers\r\n{\r\n    public partial interface I");
            
            #line 19 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName));
            
            #line default
            #line hidden
            this.Write("ApiController\r\n    {\r\n");
            
            #line 21 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 foreach(var entity in this.Entities) { 
            
            #line default
            #line hidden
            this.Write("        DataTablesResultsBase Get");
            
            #line 21 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write("s(DataTablesRequestBase request, string cacheKey);\r\n");
            
            #line 22 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    }\r\n\r\n    [Route(\"api\")]\r\n\tpublic partial class ");
            
            #line 26 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName));
            
            #line default
            #line hidden
            this.Write("ApiController : t4mvcController, I");
            
            #line 26 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName));
            
            #line default
            #line hidden
            this.Write("ApiController\r\n\t{\r\n        IServiceProvider serviceProvider;\r\n");
            
            #line 29 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 foreach(var entity in this.Entities) { 
            
            #line default
            #line hidden
            this.Write("        I");
            
            #line 30 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name));
            
            #line default
            #line hidden
            this.Write("ViewModelService ");
            
            #line 30 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("ViewModelService;\r\n");
            
            #line 31 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n\tpublic ");
            
            #line 33 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Settings.ApplicationName));
            
            #line default
            #line hidden
            this.Write("ApiController(IServiceProvider serviceProvider, ");
            
            #line 33 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(constructorParams));
            
            #line default
            #line hidden
            this.Write(")\r\n    {\r\n        this.serviceProvider = serviceProvider;");
            
            #line 35 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 foreach(var entity in this.Entities) { 
            
            #line default
            #line hidden
            this.Write("        this.");
            
            #line 36 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("ViewModelService = ");
            
            #line 36 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.Name.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("ViewModelService;\r\n");
            
            #line 37 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 } 
            
            #line default
            #line hidden
            this.Write("    }\r\n\r\n");
            
            #line 39 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 foreach(var entity in this.Entities)
		{
		var entityName = entity.Name.ToSchemaName();
		var entityCamelCase = entityName.ToCamelCase(); 
            
            #line default
            #line hidden
            this.Write("        [Route(\"get");
            
            #line 43 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName.ToLowerCase()));
            
            #line default
            #line hidden
            this.Write("s\")]\r\n        public DataTablesResultsBase Get");
            
            #line 44 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write("s(DataTablesRequestBase request, string cacheKey)\r\n        {\r\n            Current" +
                    ".SetDataTablesParameters(nameof(Get");
            
            #line 46 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write("s), cacheKey, request);\r\n\r\n            var response = new DataTablesResultsBase()" +
                    " { draw = request.Draw };\r\n\r\n            var queryBase = ");
            
            #line 50 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityCamelCase));
            
            #line default
            #line hidden
            this.Write("ViewModelService.GetAll");
            
            #line 50 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.PluralName.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write("();\r\n\r\n");
            
            #line 52 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"

var searchableFields = entity.Fields.Where(x=> x.IsSearchable);
if (searchableFields.Count() > 0)
{
 
            
            #line default
            #line hidden
            this.Write("            if (request.Search != null && request.Search.Value != null)\r\n        " +
                    "    {\r\n                var s = request.Search.Value;\r\n");
            
            #line 60 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 if (entity.HasIntSearchableFields) { 
            
            #line default
            #line hidden
            this.Write("                int i;\r\n                if (int.TryParse(s, out i))\r\n            " +
                    "    {\r\n                    queryBase = queryBase.Where(x => ");
            
            #line 64 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(" || ", searchableFields.Select(x => $"x.{(x.References == null ? (x.IsNumber ? x.Name.ToSchemaName() + " == i" : x.Name.ToSchemaName() + $".{x.SearchOperator}(s)") : x.Name.ToSchemaName() + x.References.NameField.Name.ToSchemaName() + $".{x.SearchOperator}(s)" )}"))));
            
            #line default
            #line hidden
            this.Write(");\r\n                }\r\n                else\r\n                {\r\n                 " +
                    "   queryBase = queryBase.Where(x => ");
            
            #line 68 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(" || ", searchableFields.Where(x => !x.IsNumber).Select(x => $"x.{(x.References == null ? x.Name.ToSchemaName() : x.Name.ToSchemaName() + x.References.NameField.Name.ToSchemaName() )}.{x.SearchOperator}(s)"))));
            
            #line default
            #line hidden
            this.Write(");\r\n                }\r\n");
            
            #line 70 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 } else { 
            
            #line default
            #line hidden
            this.Write("                queryBase = queryBase.Where(x => ");
            
            #line 71 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(" || ", searchableFields.Select(x => $"x.{(x.References == null ? x.Name.ToSchemaName() : x.Name.ToSchemaName() + x.References.NameField.Name.ToSchemaName() )}.{x.SearchOperator}(s)"))));
            
            #line default
            #line hidden
            this.Write(");\r\n");
            
            #line 72 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 } 
            
            #line default
            #line hidden
            this.Write("            }\r\n");
            
            #line 74 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 } 
            
            #line default
            #line hidden
            this.Write(@"            var query = queryBase.Sort(request)
                                 .Filter(request);

            var data = query.Skip(request.Start)
                            .Take(request.Length)
                            .ToList();

            var totalRecords            = queryBase.Count();
            response.recordsTotal       = totalRecords;
            response.recordsFiltered    = totalRecords;
            response.data               = data;

            return response;

        }

");
            
            #line 91 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 if (entity.NameField != null) { 
            
            #line default
            #line hidden
            this.Write("        // Select2 field\r\n        [HttpGet]\r\n        [Route(\"select2/get");
            
            #line 94 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName.ToLowerCase()));
            
            #line default
            #line hidden
            this.Write("s\")]\r\n        public IEnumerable<");
            
            #line 95 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write("ViewModel> Select2");
            
            #line 95 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName));
            
            #line default
            #line hidden
            this.Write("s(Select2SearchParameter searchParameter)\r\n        {\r\n            var records = ");
            
            #line 97 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entityName.ToCamelCase()));
            
            #line default
            #line hidden
            this.Write("ViewModelService.GetAll");
            
            #line 97 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.PluralName.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write("();\r\n\r\n            if (searchParameter != null && searchParameter.Query != null)\r" +
                    "\n            {\r\n                records = records.Where(x => x.");
            
            #line 101 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(entity.NameField.Name.ToSchemaName()));
            
            #line default
            #line hidden
            this.Write(".StartsWith(searchParameter.Query));\r\n            }\r\n\r\n            return records" +
                    ".ToList();\r\n        }\r\n");
            
            #line 106 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
 } 
            
            #line default
            #line hidden
            
            #line 107 "C:\dev\git\t4mvc\scaffolding\t4mvc.scaffolding\templates\apimethodset.tt"
}
            
            #line default
            #line hidden
            this.Write("\r\n\t}\r\n}\r\n");
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
    public class apimethodsetBase
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
