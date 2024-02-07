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
        public async Task<ServiceResponse<AccountSignupResDto>> AddAccount(AccountSignupReqDto req)
        {
            ServiceResponse<AccountSignupResDto> res = new();

            Account account = await _accountRepository.GetAccountByAccountnameAsync(req.AccountName);

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

                bool isAddAccountSucced = await _accountRepository.AddAccount(newAccount);
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

        public async Task<ServiceResponse<AccountLoginResDto>> AccountLogin(AccountLoginReqDto req)
        {
            ServiceResponse<AccountLoginResDto> res = new();

            Account account = await _accountRepository.GetAccountByAccountnameAsync(req.AccountName);

            if (account != null && _passwordEncryptor.IsmatchPassword(req.Password, account.Password))
            {
                res.Success = true;
                res.Data = _mapper.Map<AccountLoginResDto>(account);
                res.Message = "ok";
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
