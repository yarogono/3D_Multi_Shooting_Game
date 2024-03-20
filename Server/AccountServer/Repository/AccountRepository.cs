using AccountServer.Entities;
using AccountServer.Repository.Contract;
using AccountServer.Utils;
using SqlKata.Execution;
using ZLogger;

namespace AccountServer.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ILogger<AccountRepository> _logger;
        private readonly QueryFactory _queryFactory;

        public AccountRepository(ILogger<AccountRepository> logger, QueryFactory queryFactory)
        {
            _logger = logger;
            _queryFactory = queryFactory;

            _queryFactory.Connection.Open();
        }

        public void Dispose()
        {
            this._queryFactory.Connection.Close();
        }

        public async Task<bool> AddAccount(Account account)
        {
            try
            {
                int count = await _queryFactory.Query("account").InsertAsync(account);

                if (count == 0)
                {
                    _logger.ZLogError(
                        $"[AccountDb.AddAccount] ErrorCode: {ErrorCode.CreateAccountFailException}, AccountId: {account.AccountId}");
                }

                return count != 1 ? false : true;
            }
            catch (Exception ex)
            {
                _logger.ZLogError(ex,
                    $"[AccountDb.AddAccount] ErrorCode: {ErrorCode.CreateAccountFailException}, AccountId: {account.AccountId}");
            }

            return false;
        }

        public async Task<Account> GetAccountByAccountname(string accountname)
        {
            Account account = null;
            try
            {
                account = await _queryFactory.Query("account").Where("AccountName", accountname).FirstOrDefaultAsync<Account>();
            }
            catch (Exception ex)
            {
                _logger.ZLogError(ex,
                    $"[AccountDb.GetAccountByAccountname] ErrorCode: {ErrorCode.AccountIdMismatch}, AccountName: {accountname}");
            }

            return account;
        }

        public async void UpdateAccountLastLogin(int accountId)
        {
            try
            {
                int count = await _queryFactory.Query("account").Where("accountId", accountId).UpdateAsync(new
                {
                    CreatedAt = DateTime.Now,
                });

                if (count == 0)
                {
                    _logger.ZLogError(
                        $"[AccountDb.AccountUpdateException] ErrorCode: {ErrorCode.AccountUpdateException} AccountId: {accountId}");
                }
            }
            catch (Exception ex)
            {
                _logger.ZLogError(ex,
                    $"[AccountDb.AccountUpdateException] ErrorCode: {ErrorCode.AccountUpdateException} AccountId: {accountId}");
            }
        }
    }
}
