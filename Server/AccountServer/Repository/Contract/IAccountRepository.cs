using AccountServer.Entities;
using AccountServer.Utils;

namespace AccountServer.Repository.Contract;

public interface IAccountRepository : IDisposable
{
    public void UpdateAccountLastLogin(int accountId);

    public Task<Account> GetAccountByAccountname(string accountname);

    public Task<bool> AddAccount(Account account);
}
