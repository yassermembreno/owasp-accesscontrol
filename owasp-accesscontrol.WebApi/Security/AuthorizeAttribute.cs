using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace owasp_accesscontrol.WebApi.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Method.Equals("Get", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            IIdentity? identity = context.HttpContext.User.Identity;
                        
            if (identity == null || !identity.IsAuthenticated)
            {
                context.Result = new JsonResult(new
                {
                    successful = false,
                    error = "Sorry, something went wrong.",
                    message = "Unauthorized User."

                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized

                };
            }

            return;
        }
    }
}

