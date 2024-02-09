using AccountServer.Entities;
using AccountServer.Repository.Contract;
using AccountServer.Service.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AccountServer.Service
{
    public class OauthService : IOauthService
    {
        private readonly IOauthRepository _oauthRepository;
        private readonly IAccountRepository _accountRepository;

        public OauthService(IOauthRepository oauthRepository, IAccountRepository accountRepository)
        {
            _oauthRepository = oauthRepository;
            _accountRepository = accountRepository;
        }

        public async Task<ServiceResponse<GoogleLoginResDto>> GoogleLogin(AuthenticateResult result, string token)
        {
            ServiceResponse<GoogleLoginResDto> res = new();

            if (result.Succeeded == true)
            {
                ClaimsPrincipal principal = result.Principal;

                string email = principal.FindFirstValue(ClaimTypes.Email);
                string name = principal.FindFirstValue(ClaimTypes.Name);
                string accessToken = result.Properties.GetTokenValue("access_token");

                Account account = _accountRepository.GetAccountByAccountname(email);
                if (account != null)
                {
                    res.Success = true;
                    res.Message = "Google Login";
                    res.Data = new GoogleLoginResDto()
                    {
                        email = email,
                        name = name,
                    };
                    _accountRepository.UpdateAccountLastLogin(account);
                    return res;
                }

                res.Success = true;
                res.Message = "Google Login";
                res.Data = new GoogleLoginResDto()
                {
                    email = email,
                    name = name,
                };

                Account newAccount = new Account()
                {
                    AccountName = email,
                    CreatedAt = DateTime.Now,
                    Nickname = name,
                    Password = "",
                };

                Oauth newOauth = new Oauth()
                {
                    OauthToken = accessToken,
                    OauthType = Utils.Define.OauthType.Google,
                    Account = newAccount,
                };

                _oauthRepository.AddAccountOauth(newOauth, newAccount);
                _accountRepository.UpdateAccountLastLogin(newAccount);
            }
            else
            {
                res.Success = false;
                res.Message = "Google Login Failed";
                res.Error = result.Failure.Message;
            }

            return res;
        }
    }
}