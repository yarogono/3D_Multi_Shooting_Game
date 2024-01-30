using AccountServer.DB;
using AccountServer.Dto.PlayerLogin;
using AccountServer.Model;
using AccountServer.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordEncryptor _passwordEncryptor;

        public PlayerController(AppDbContext context, PasswordEncryptor passwordEncryptor)
        {
            _context = context;
            _passwordEncryptor = passwordEncryptor;
        }

        [HttpPost]
        [Route("login")]
        public PlayerLoginResDto Login([FromBody] PlayerLoginReqDto req)
        {
            PlayerDb player = _context.Player
                    .AsNoTracking()
                    .Where(p => p.Nickname == req.PlayerAccountName)
                    .FirstOrDefault();

            PlayerLoginResDto res = new PlayerLoginResDto();
            if (player == null || _passwordEncryptor.IsmatchPassword(req.PlayerPassword, player.Password))
            {
                res.IsLoginSucceed = false;
                return res;
            }

            res.PlayerId = player.PlayerId;
            res.IsLoginSucceed = true;
            return res;
        }

        [HttpPost]
        [Route("signup")]
        public PlayerSignupResDto Signup([FromBody] PlayerSignupReqDto req)
        {

            return null;
        }
    }
}
