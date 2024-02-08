using AccountServer.Entities;

namespace AccountServer.Repository.Contract
{
    public interface IAccountRepository
    {
        void UpdateAccountLastLogin(Account account);

        Account GetAccountByAccountname(string accountname);

        bool AddAccount(Account account);
    }
}
