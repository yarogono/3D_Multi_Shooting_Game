using Microsoft.AspNetCore.Authentication;

namespace AccountServer.Service.Contract
{
    public interface IOauthService
    {
        public Task<ServiceResponse<GoogleLoginResDto>> GoogleLogin(AuthenticateResult result, string token);
    }
}
