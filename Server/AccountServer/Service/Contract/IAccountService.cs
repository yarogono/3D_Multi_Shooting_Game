namespace AccountServer.Service.Contract
{
    public interface IAccountService
    {
        public ServiceResponse<AccountSignupResDto> AddAccount(AccountSignupReqDto req);

        public ServiceResponse<AccountLoginResDto> AccountLogin(AccountLoginReqDto req);
    }
}
