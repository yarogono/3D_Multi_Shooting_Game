using Microsoft.AspNetCore.Authentication;

namespace AccountServer.Service.Contract
{
    public interface IOauthService
    {
        Task<ServiceResponse<GoogleLoginResDto>> GoogleLogin(AuthenticateResult result);
    }
}
