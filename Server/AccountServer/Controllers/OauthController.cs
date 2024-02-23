using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using AccountServer.Service.Contract;
using Microsoft.AspNetCore.Authentication.Google;

namespace AccountServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OauthController : Controller
    {

        private readonly IOauthService _oauthService;

        public OauthController(IOauthService oauthService)
        {
            _oauthService = oauthService;
        }

        [HttpGet]
        [Route("google-login")]
        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        [HttpGet]
        public async Task<ActionResult<GoogleLoginResDto>> GoogleResponse()
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();
            string token = await HttpContext.GetTokenAsync("access_token");

            var res = _oauthService.GoogleLogin(result, token);
            
            return Ok(res);
        }
    }
}
