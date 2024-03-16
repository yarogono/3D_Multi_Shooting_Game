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
    public class OauthRepository : IOauthRepository
    {
        private readonly ILogger<OauthRepository> _logger;
        QueryFactory _queryFactory;

        public OauthRepository(ILogger<OauthRepository> logger, QueryFactory queryFactory)
        {
            _logger = logger;
            _queryFactory = queryFactory;
        }

        public void Dispose()
        {
            this._queryFactory.Dispose();
        }
        public bool AddAccountOauth(Oauth oauth, Account account)
        {
            try
            {
                int count = _queryFactory.Query("oauth").Insert(oauth);

                if (count == 0)
                {
                    _logger.ZLogError(
                        $"[AccountDb.AddAccountOauthFail] ErrorCode: {ErrorCode.AddAccountOauthFail} AccountId: {account.AccountId}");
                }
            }
            catch (Exception ex)
            {
                _logger.ZLogError(ex,
                    $"[AccountDb.AddAccountOauthFail] ErrorCode: {ErrorCode.AddAccountOauthFail}  AccountId:  {account.AccountId}");
            }

            return true;
        }
    }
}
