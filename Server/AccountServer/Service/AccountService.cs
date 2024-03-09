using AccountServer.Entities;
using AccountServer.Service.Contract;
using AccountServer.Utils;
using AccountServer.Repository.Contract;
using AutoMapper;

namespace AccountServer.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly IMapper _mapper;

        public AccountService(PasswordEncryptor passwordEncryptor, IAccountRepository accountRepository, IMapper mapper)
        {
            _passwordEncryptor = passwordEncryptor;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public ServiceResponse<AccountSignupResDto> AddAccount(AccountSignupReqDto req)
        {
            ServiceResponse<AccountSignupResDto> res = new();

            Account account = _accountRepository.GetAccountByAccountname(req.AccountName).Result;

            if (account == null)
            {
                string encryptPassword = _passwordEncryptor.Encrypt(req.Password);

                Account newAccount = new Account()
                {
                    AccountName = req.AccountName,
                    Password = encryptPassword,
                    Nickname = req.Nickname,
                    CreatedAt = DateTime.Now,
                };

                bool isAddAccountSucced = _accountRepository.AddAccount(newAccount).Result;
                if (isAddAccountSucced == false)
                {
                    res.Error = "RepoError";
                    res.Success = false;
                    res.Data = null;
                    return res;
                }

                res.Success = true;
                res.Data = _mapper.Map<AccountSignupResDto>(newAccount);
                res.Message = "Created";
            }
            else
            {
                res.Message = "Duplicated Account";
                res.Success = false;
                res.Data = null;
            }

            return res;
        }

        public ServiceResponse<AccountLoginResDto> AccountLogin(AccountLoginReqDto req)
        {
            ServiceResponse<AccountLoginResDto> res = new();

            Account account = _accountRepository.GetAccountByAccountname(req.AccountName).Result;

            if (account != null && _passwordEncryptor.IsmatchPassword(req.Password, account.Password))
            {
                res.Success = true;
                res.Data = _mapper.Map<AccountLoginResDto>(account);
                res.Message = "ok";

                _accountRepository.UpdateAccountLastLogin(account);
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
