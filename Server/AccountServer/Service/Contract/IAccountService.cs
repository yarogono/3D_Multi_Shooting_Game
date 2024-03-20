namespace AccountServer.Service.Contract
{
    public interface IAccountService
    {
        public Task<ServiceResponse<AccountSignupResDto>> AddAccount(AccountSignupReqDto req);

        public Task<ServiceResponse<AccountLoginResDto>> AccountLogin(AccountLoginReqDto req);
    }
}
