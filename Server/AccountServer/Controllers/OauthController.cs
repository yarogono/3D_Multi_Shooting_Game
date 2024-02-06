using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using AccountServer.Service.Contract;

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
        public async Task<ActionResult<GoogleLoginResDto>> GoogleResponse()
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync();

            var res = _oauthService.GoogleLogin(result);
            
            return Ok(res.Result);
        }
    }
}
