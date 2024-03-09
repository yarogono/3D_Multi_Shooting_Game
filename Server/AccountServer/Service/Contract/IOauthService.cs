using Microsoft.AspNetCore.Authentication;

namespace AccountServer.Service.Contract
{
    public interface IOauthService
    {
        public ServiceResponse<GoogleLoginResDto> GoogleLogin(AuthenticateResult result, string token);
    }
}
