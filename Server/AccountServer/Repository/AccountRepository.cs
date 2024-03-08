using AccountServer.DB;
using AccountServer.Entities;
using AccountServer.Repository.Contract;

namespace AccountServer.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _dataContext;

        public AccountRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public Account GetAccountByAccountname(string accountname)
        {
            return _dataContext.Accounts.FirstOrDefault(a => a.AccountName == accountname);
        }

        public bool AddAccount(Account account)
        {
            _dataContext.Accounts.Add(account);
            return Save();
        }
        public void UpdateAccountLastLogin(Account account)
        {
            account.LastLoginAt = DateTime.Now;

            Save();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() >= 0 ? true : false;
        }
    }
}
