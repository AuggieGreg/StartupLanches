using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupLanches
{
    public static class Extensions
    {
        public static bool IsAjaxRequest(this HttpContext context)
        {
            if (context == null || context.Request == null)
                throw new ArgumentNullException(nameof(context));
            if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return true;
            return false;
        }
    }
}
