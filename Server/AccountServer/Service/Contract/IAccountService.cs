namespace AccountServer.Service.Contract
{
    public interface IAccountService
    {
        Task<ServiceResponse<AccountSignupResDto>> AddAccount(AccountSignupReqDto req);

        Task<ServiceResponse<AccountLoginResDto>> AccountLogin(AccountLoginReqDto req);
    }
}
