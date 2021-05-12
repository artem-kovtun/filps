using System.Linq;
using System.Threading.Tasks;
using Filps.Application.Requests.Users.Queries;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filps.Controllers
{
    [Route("api/users")]
    public class UserController : MediatorController
    {
        [HttpGet("authentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthentication() 
        {
            return Ok(await Mediator.Send(new GetUserAuthenticationQuery(Email)));
        }
        
        [HttpGet("externalLogin")]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var callbackUrl = Url.Action("ExternalLoginCallback", new {returnUrl});
            var authenticationProperties = new AuthenticationProperties { RedirectUri = callbackUrl };
            return Challenge(authenticationProperties, provider);
        }
        
        [HttpGet("externalLoginCallback")]
        public IActionResult ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            return Redirect(returnUrl);
        }
    }
}