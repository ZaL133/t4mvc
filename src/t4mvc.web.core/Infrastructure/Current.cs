using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using t4mvc.web.core.Models;

namespace t4mvc.web.core.Infrastructure
{
    public class Current
    {
        const string    editModeKey     = "__EditMode", 
                        returnUrlParam  = "ReturnUrl";

        private static IHttpContextAccessor httpContextAccessor;
        private static IActionContextAccessor actionContextAccessor;
        private static IServiceProvider servicePro;

        public static IMapper Mapper { get; set; }

        private static IMemoryCache memoryCache;
        private static IWebHostEnvironment webHostEnvironment;
        private static ILog logger = LogManager.GetLogger(typeof(t4mvc.web.core.Infrastructure.Current));
        public static ILog Logger => logger;


        public static void Configure(IServiceProvider serviceProvider)
        {
            servicePro              = serviceProvider;
            httpContextAccessor     = serviceProvider.GetService<IHttpContextAccessor>();
            actionContextAccessor   = serviceProvider.GetService<IActionContextAccessor>();
            Mapper                  = serviceProvider.GetService<IMapper>();
            memoryCache             = serviceProvider.GetService<IMemoryCache>();
            webHostEnvironment      = serviceProvider.GetService<IWebHostEnvironment>();
        }

        public static HttpContext Context
        {
            get
            {
                return httpContextAccessor.HttpContext;
            }
        }

        public static IUrlHelper UrlHelper
        {
            get
            {
                return servicePro.GetService<IUrlHelper>();
            }
        }

        public static Guid UserId
        {
            get
            {
                return Guid.Parse(Current.Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }

        public static bool EditMode
        {
            get
            {
                var editMode = Current.Context.Items[editModeKey];
                return editMode == null ? false : (bool)editMode;
            }
            set
            {
                Current.Context.Items[editModeKey] = value;
            }
        }

        /// <summary>
        /// Either Save -or- Save and close
        /// </summary>
        public static string Action
        {
            get
            {
                return Current.Context.Submitted("FormAction");
            }
        }

        public static string ReturnUrl
        {
            get
            {
                var returnUrl = Current.Context.Submitted(returnUrlParam).FirstOrDefault();
                if (returnUrl != null && returnUrl.Contains("://"))
                {
                    Current.Logger.Error("Invalid uri " + returnUrl + " from page " + Current.Context.Request.Path);
                    return null;
                }

                return returnUrl;
            }
        }

        public static string RequestUrl
        {
            get
            {
                return Current.Context.Request.Path.Value;
            }
        }
        public static string ReturnUrlEncoded
        {
            get
            {
                var rUrl = ReturnUrl;
                return rUrl == null ? null : HttpUtility.UrlEncode(rUrl);
            }
        }

        public static string PathAndQuery
        {
            get
            {
                var path = Context.Request.Path;
                var query = Context.Request.QueryString;
                return $"{path}{query}";
            }
        }

        public static ActionResult GetEditDestination(Func<string> saveDestination, Func<string> saveAndCloseDestination)
        {
            if (Current.Action == "Save")
            {
                return new RedirectResult(saveDestination() ?? Current.ReturnUrl);
            }
            if (Current.Action == "Save and close")
            {
                return new RedirectResult(saveAndCloseDestination());
            }

            throw new ArgumentException("Action not found");
        }

        public static RedirectResult GetCreateDestination(Func<string> saveDestination, Func<string> saveAndCloseDestination)
        {
            if (Current.Action == "Save")
            {
                return new RedirectResult(saveDestination() ?? Current.ReturnUrl);
            }
            if (Current.Action == "Save and close")
            {
                return new RedirectResult(Current.ReturnUrl ?? saveAndCloseDestination());
            }

            throw new ArgumentException("Action not found");
        }

        private static string GetDataTableParametersKey(string apiMethod, string cacheKey)
        {
            var userId = Current.UserId;
            var result = $"{userId}-{apiMethod}-{cacheKey}-dataTables-key";
            return result;
        }
        public static DataTablesRequestBase GetDataTablesParameters(string apiMethod, string cacheKey)
        {
            var key = GetDataTableParametersKey(apiMethod, cacheKey);
            var cache = memoryCache;
            return cache.Get(key) as DataTablesRequestBase;
        }

        public static DataTablesRequestBase GetMaxLengthDataTablesParameters(string apiMethod, string cacheKey)
        {
            var rv = GetDataTablesParameters(apiMethod, cacheKey);
            if (rv != null) rv.Length = Settings.DATATABLEMAXLENGTH;
            return rv;
        }
        public static void SetDataTablesParameters(string apiMethod, string cacheKey, DataTablesRequestBase request)
        {
            var key = GetDataTableParametersKey(apiMethod, cacheKey);
            var cache = memoryCache;
            cache.Set(key, request, TimeSpan.FromMinutes(30));
        }

        public static IFileInfo MapPath(string path)
        {
            return webHostEnvironment.WebRootFileProvider.GetFileInfo(path);
        }

    }

    public static class HttpContextExtensions
    {
        public static StringValues Submitted(this HttpContext context, string key)
        {
            return context.Request.Submitted(key);
        }

        public static StringValues Submitted(this HttpRequest request, string key)
        {
            if (request.Method == "POST" && request.Form.ContainsKey(key))
                return request.Form[key];

            if (request.Query.ContainsKey(key))
                return request.Query[key];

            if (request.Cookies.ContainsKey(key))
                return request.Cookies[key];

            return StringValues.Empty;
        }
    }
}
