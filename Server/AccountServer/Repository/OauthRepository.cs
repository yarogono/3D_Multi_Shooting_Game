using AccountServer.Entities;
using AccountServer.Repository.Contract;
using AccountServer.Utils;
using Dapper;
using SqlKata.Execution;
using ZLogger;

namespace AccountServer.Repository
{
    public class OauthRepository : IOauthRepository
    {
        private readonly ILogger<OauthRepository> _logger;
        private readonly QueryFactory _queryFactory;

        public OauthRepository(ILogger<OauthRepository> logger, QueryFactory queryFactory)
        {
            _logger = logger;
            _queryFactory = queryFactory;

            _queryFactory.Connection.Open();
        }

        public void Dispose()
        {
            this._queryFactory.Connection.Close();
        }

        public bool AddAccountOauth(Oauth oauth, Account account)
        {
            using var transaction = _queryFactory.Connection.BeginTransaction();
            try
            {
                int accountId = _queryFactory.Query("account").InsertGetId<int>(account, transaction);

                oauth.AccountId = accountId;
                _queryFactory.Query("oauth").Insert(oauth, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.ZLogError(ex,
                    $"[AccountDb.AddAccountOauthFail] ErrorCode: {ErrorCode.AddAccountOauthFail}  AccountName:  {account.AccountName}");
            }

            return true;
        }
    }
}
