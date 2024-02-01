using AccountServer.Model;
using AccountServer.Service.Contract;
using AccountServer.Utils;
using AccountServer.Repository.Contract;
using System.Security.Principal;

namespace AccountServer.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly PasswordEncryptor _passwordEncryptor;

        public AccountService(PasswordEncryptor passwordEncryptor, IAccountRepository accountRepository)
        {
            _passwordEncryptor = passwordEncryptor;
            _accountRepository = accountRepository;
        }
        public async Task<ServiceResponse<AccountSignupResDto>> AddAccount(AccountSignupReqDto req)
        {
            ServiceResponse<AccountSignupResDto> res = new();

            Account account = await _accountRepository.GetAccountByAccountnameAsync(req.AccountName);

            if (account == null)
            {
                string encryptPassword = _passwordEncryptor.Encrypt(req.Password);

                //Account newAccount = new Account()
                //{
                //    AccountName = req.AccountName,
                //    Password = encryptPassword,
                //    Nickname = req.Nickname,
                //    CreatedAt = DateTime.Now,
                //};
                //EntityEntry<Account> addedAccount = _context.Accounts.Add(newAccount);

                //_context.SaveChanges();

                //res.AccountId = addedAccount.Entity.AccountId;
                //res.IsSignupSucceed = true;
            }
            else
            {
                //res.IsSignupSucceed = false;
            }

            return res;
        }

        public async Task<ServiceResponse<AccountLoginResDto>> AccountLogin(AccountLoginReqDto req)
        {
            ServiceResponse<AccountLoginResDto> res = new();

            Account account = await _accountRepository.GetAccountByAccountnameAsync(req.AccountName);

            if (account != null || _passwordEncryptor.IsmatchPassword(req.Password, account.Password))
            {
                res.Success = true;
                //res.Data = 
                //res.AccountId = account.AccountId;
                //res.Nickname = account.Nickname;
                //res.IsLoginSucceed = true;
            }
            else
            {
                res.Success = false;
                res.Data = null;
                res.Message = "Can't find Account";
            }

            return res;
        }

    }
}
