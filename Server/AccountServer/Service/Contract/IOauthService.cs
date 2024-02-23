using Microsoft.AspNetCore.Authentication;

namespace AccountServer.Service.Contract
{
    public interface IOauthService
    {
        ServiceResponse<GoogleLoginResDto> GoogleLogin(AuthenticateResult result, string token);
    }
}
