

using AccountServer.Entities;

namespace AccountServer.Repository.Contract
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByAccountnameAsync(string accountname);

        Task<bool> AddAccount(Account account);
    }
}
