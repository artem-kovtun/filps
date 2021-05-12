using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filps.Attributes
{
    public sealed class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {        
            var allowUnauthorized = context.ActionDescriptor.EndpointMetadata.Any(item => item is AllowAnonymousAttribute);
            
            var tokenHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (tokenHeader == null)
            {
                context.Result = !allowUnauthorized ? new UnauthorizedObjectResult(null) : null;
                return;
            }
            
            var jwt = tokenHeader?.Substring(7);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            if (token == null)
            {
                context.Result = !allowUnauthorized ? new UnauthorizedObjectResult(null) : null;
                return;
            };

            context.HttpContext.Items["Email"] = token.Claims.FirstOrDefault(t => t.Type == "email")?.Value;
        }
    }
}