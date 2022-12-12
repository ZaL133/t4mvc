using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.data;

namespace t4mvc.web.core.Infrastructure
{
    public interface IContextLocator
    {
        t4DbContext GetContext();
    }

    internal class HttpContextLocator : IContextLocator
    {
        const string HttpContextKey = "t4DbContextKey";
        public t4DbContext GetContext()
        {
            if (Current.Context != null)
            {
                if (Current.Context.Items[HttpContextKey] == null)
                {
                    Current.Context.Items[HttpContextKey] = new t4DbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<t4DbContext>());
                }

                return Current.Context.Items[HttpContextKey] as t4DbContext;
            }

            throw new InvalidOperationException("HttpContext can not be null");
        }

    }
}
