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
        readonly IOptions<DbConfig> _dbConfig;
        readonly ILogger<OauthRepository> _logger;

        IDbConnection _dbConn;
        SqlKata.Compilers.MySqlCompiler _compiler;
        QueryFactory _queryFactory;

        public OauthRepository(ILogger<OauthRepository> logger, IOptions<DbConfig> dbConfig)
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
