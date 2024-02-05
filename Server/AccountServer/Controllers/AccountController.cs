using AccountServer.Service.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace AccountServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<AccountSignupResDto>> Signup([FromBody] AccountSignupReqDto req)
        {
            var res = await _accountService.AddAccount(req);
            return Ok(res);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AccountLoginResDto>> Login([FromBody] AccountLoginReqDto req)
        {
            var res = await _accountService.AccountLogin(req);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("test")]
        public IEnumerable<string> TestGet()
        {
            List<string> testList = new List<string>()
            {
                "Test1", "Test2", "Test3"
            };

            return testList;
        }

        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-reponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync();

            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            return Json(claims);
        }
    }
}
