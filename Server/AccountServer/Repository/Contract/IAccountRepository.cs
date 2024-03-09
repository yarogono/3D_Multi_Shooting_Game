using AccountServer.Entities;
using AccountServer.Utils;

namespace AccountServer.Repository.Contract;

public interface IAccountRepository : IDisposable
{
    public Task<ErrorCode> UpdateAccountLastLogin(Account account);

    public Task<Account> GetAccountByAccountname(string accountname);

    public Task<bool> AddAccount(Account account);
}
