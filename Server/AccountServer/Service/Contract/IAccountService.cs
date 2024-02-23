namespace AccountServer.Service.Contract
{
    public interface IAccountService
    {
        ServiceResponse<AccountSignupResDto> AddAccount(AccountSignupReqDto req);

        ServiceResponse<AccountLoginResDto> AccountLogin(AccountLoginReqDto req);
    }
}
