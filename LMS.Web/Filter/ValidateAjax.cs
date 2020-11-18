using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LMS.Web.Filter
{
    public class ValidateAjax:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.IsAjax())
            {
                if (!context.ModelState.IsValid)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
        }
    }
}
