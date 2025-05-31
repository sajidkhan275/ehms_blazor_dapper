using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EHMSWebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("OpenIdConnect");
            await HttpContext.SignOutAsync("Cookies");
            string? postLogoutRedirectUri = _configuration.GetSection("CustomConfig").GetValue<string>("LogoutRedirectUri"); 
            string? logoutUrl = $" {_configuration.GetSection("CustomConfig").GetValue<string>("LogoutUrl")}={postLogoutRedirectUri}";
            return Redirect(logoutUrl);
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties {RedirectUri = "/" }, "OpenIdConnect");
        }
    }
}
