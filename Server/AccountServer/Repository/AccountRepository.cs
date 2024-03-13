using AccountServer.Entities;
using AccountServer.Repository.Contract;
using AccountServer.Utils;
using Microsoft.Extensions.Options;
using MySqlConnector;
using SqlKata.Execution;
using System.Data;
using ZLogger;

namespace AccountServer.Repository
{
    public class AccountRepository : IAccountRepository
    {
        readonly IOptions<DbConfig> _dbConfig;
        readonly ILogger<AccountRepository> _logger;

        IDbConnection _dbConn;
        SqlKata.Compilers.MySqlCompiler _compiler;
        QueryFactory _queryFactory;

        public AccountRepository(ILogger<AccountRepository> logger, IOptions<DbConfig> dbConfig)
        {
            _logger = logger;
            _dbConfig = dbConfig;

            Open();

            _compiler = new SqlKata.Compilers.MySqlCompiler();
            _queryFactory = new QueryFactory(_dbConn, _compiler);
        }

        private void Open()
        {
            _dbConn = new MySqlConnection(_dbConfig.Value.AccountDb);

            _dbConn.Open();
        }

        public void Dispose()
        {
            Close();
        }

        private void Close()
        {
            _dbConn.Close();
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
                account = await _queryFactory.Query("account").Where(accountname).FirstOrDefaultAsync<Account>();
            }
            catch (Exception ex)
            {
                _logger.ZLogError(ex,
                    $"[AccountDb.GetAccountByAccountname] ErrorCode: {ErrorCode.AccountIdMismatch}, AccountName: {accountname}");
            }

            return account;
        }

        public void UpdateAccountLastLogin(int accountId)
        {
            try
            {
                int count = _queryFactory.Query("account").Where("accountId", accountId).Update(new
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
