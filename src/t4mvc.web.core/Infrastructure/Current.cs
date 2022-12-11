using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;

namespace t4mvc.web.core.Infrastructure
{
    public class Current
    {
        private static IHttpContextAccessor httpContextAccessor;

        public static IMapper Mapper { get; set; }

        public static void Configure(IServiceProvider serviceProvider)
        {
            httpContextAccessor     = serviceProvider.GetService<IHttpContextAccessor>();
            Mapper                  = serviceProvider.GetService<IMapper>();
        }

        public static HttpContext Context
        {
            get
            {
                return httpContextAccessor.HttpContext;
            }
        }

        public static Guid UserId
        {
            get
            {
                return Guid.Parse(Current.Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }
    }
}
