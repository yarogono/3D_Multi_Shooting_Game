using AccountServer.DB;
using AccountServer.Model;
using AccountServer.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace AccountServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordEncryptor _passwordEncryptor;

        public AccountController(AppDbContext context, PasswordEncryptor passwordEncryptor)
        {
            _context = context;
            _passwordEncryptor = passwordEncryptor;
        }

        [HttpPost]
        [Route("login")]
        public AccountLoginResDto Login([FromBody] AccountLoginReqDto req)
        {
            AccountLoginResDto res = new AccountLoginResDto();
            if (string.IsNullOrEmpty(req.AccountName) || string.IsNullOrEmpty(req.Password))
            {
                res.IsLoginSucceed = false;
                return res;
            }    

            AccountDb account = _context.Accounts
                    .AsNoTracking()
                    .Where(a => a.Nickname == req.AccountName)
                    .FirstOrDefault();

            if (account == null || _passwordEncryptor.IsmatchPassword(req.Password, account.Password))
            {
                res.IsLoginSucceed = false;
            }
            else
            { 
                res.AccountId = account.AccountId;
                res.IsLoginSucceed = true;
            }

            return res;
        }

        [HttpPost]
        [Route("signup")]
        public AccountSignupResDto Signup([FromBody] AccountSignupReqDto req)
        {
            AccountSignupResDto res = new AccountSignupResDto();
            if (string.IsNullOrEmpty(req.AccountName) || string.IsNullOrEmpty(req.Password) || string.IsNullOrEmpty(req.ConfirmPassword) ||
                req.Password.Equals(req.ConfirmPassword) == false)
            {
                res.IsSignupSucceed = false;
                return res;
            }

            AccountDb account = _context.Accounts
                                .AsNoTracking()
                                .Where(a => a.AccountName == req.AccountName)
                                .FirstOrDefault();

            if (account == null)
            {
                string encryptPassword = _passwordEncryptor.Encrypt(req.Password);

                AccountDb newAccount = new AccountDb()
                {
                    AccountName = req.AccountName,
                    Password = encryptPassword
                };
                _context.Accounts.Add(newAccount);

                _context.SaveChanges();

                res.AccountId = newAccount.AccountId;
                res.IsSignupSucceed = true;
            }
            else
            {
                res.IsSignupSucceed = false;
            }

            return res;
        }
    }
}
