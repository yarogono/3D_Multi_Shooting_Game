using AccountServer.Service.Contract;
using Microsoft.AspNetCore.Mvc;

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
            var res = _accountService.AddAccount(req);
            return Ok(res);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AccountLoginResDto>> Login([FromBody] AccountLoginReqDto req)
        {
            var res = _accountService.AccountLogin(req);
            return Ok(res);
        }
    }
}
