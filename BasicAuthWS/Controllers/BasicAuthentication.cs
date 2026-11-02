
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text;

namespace BasicAuthWS.Controllers
{
    public class BasicAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string authHeader =
                context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(authHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string[] authHeaderParts = authHeader.Split(' ', 2,
                StringSplitOptions.RemoveEmptyEntries);

            if (authHeaderParts.Length != 2 ||
                !authHeaderParts[0].Equals("Basic",
                    StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                string credentials = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authHeaderParts[1]));

                string[] parts = credentials.Split(':', 2);

                if (parts.Length != 2 ||
                    !parts[0].Equals("instructor01",
                        StringComparison.OrdinalIgnoreCase) ||
                    parts[1] != "Password01")
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                base.OnActionExecuting(context);
            }
            catch (FormatException)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
