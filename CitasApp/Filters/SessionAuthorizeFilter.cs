using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace CitasApp.Web.Filters
{
    public class SessionAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata
                .Any(m => m is AllowAnonymousAttribute))
                return;

            if (context.HttpContext.Session.GetInt32("UsuarioId") == null)
                context.Result = new RedirectToActionResult("Index", "Login", null);
        }
    }
}
