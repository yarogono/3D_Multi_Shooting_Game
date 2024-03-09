using AccountServer.Entities;
using AccountServer.Repository.Contract;
using Microsoft.Extensions.Options;
using MySqlConnector;
using SqlKata.Execution;
using System.Data;

namespace AccountServer.Repository
{
    public class OauthDb : IOauthRepository
    {
        readonly IOptions<DbConfig> _dbConfig;
        readonly ILogger<OauthDb> _logger;

        IDbConnection _dbConn;
        SqlKata.Compilers.MySqlCompiler _compiler;
        QueryFactory _queryFactory;

        public OauthDb(ILogger<OauthDb> logger, IOptions<DbConfig> dbConfig)
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

            }
            catch (Exception ex)
            {

            }

            return true;
        }

    }
}
