using AccountServer.Repository.Contract;
using AccountServer.Service.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Principal;

namespace AccountServer.Service
{
    public class OauthService : IOauthService
    {
        private readonly IOauthRepository _oauthRepository;
        private readonly IMapper _mapper;

        public OauthService(IOauthRepository oauthRepository)
        {
            _oauthRepository = oauthRepository;
        }

        public async Task<ServiceResponse<GoogleLoginResDto>> GoogleLogin(AuthenticateResult result)
        {
            ServiceResponse<GoogleLoginResDto> res = new();

            if (result.Succeeded == true)
            {
                ClaimsPrincipal principal = result.Principal;

                string email = principal.FindFirstValue(ClaimTypes.Email);
                string name = principal.FindFirstValue(ClaimTypes.Name);

                res.Success = true;
                res.Message = "Google Login";
                res.Data = new GoogleLoginResDto()
                {
                    email = email,
                    name = name,
                };
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
