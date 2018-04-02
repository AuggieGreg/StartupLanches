using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StartupLanches.Models;

namespace StartupLanches
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //Identifica se é requisição Ajax, se for, retorna um JSON com os dados do erro, do contrário, passa.
            if (context.HttpContext.IsAjaxRequest())
            {
                var result = new ObjectResult(new
                {
                    code = 500,
                    message = context.Exception.Message,
                    stackTrace = context.Exception.StackTrace,
                    type = context.Exception.GetType().Name
                });

                result.StatusCode = 500;
                context.Result = result;
            }
        }
    }
}