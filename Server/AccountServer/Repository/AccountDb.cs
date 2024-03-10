using AccountServer.Entities;
using AccountServer.Repository.Contract;
using Microsoft.Extensions.Options;
using MySqlConnector;
using SqlKata.Execution;
using System.Data;

namespace AccountServer.Repository;

public class AccountDb : IAccountRepository
{
    readonly IOptions<DbConfig> _dbConfig;
    readonly ILogger<AccountDb> _logger;

    IDbConnection _dbConn;
    SqlKata.Compilers.MySqlCompiler _compiler;
    QueryFactory _queryFactory;

    public AccountDb(ILogger<AccountDb> logger, IOptions<DbConfig> dbConfig)
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

            return count != 1 ? false : true;
        }
        catch (Exception ex)
        {

        }

        return false;
    }

    public Task<Account> GetAccountByAccountname(string accountname)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }

        return null;
    }

    public void UpdateAccountLastLogin(Account account)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }
}
