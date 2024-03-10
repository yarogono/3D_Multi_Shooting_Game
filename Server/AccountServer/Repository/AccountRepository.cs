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

        public async Task<Account> GetAccountByAccountname(string accountname)
        {
            Account account = _dataContext.Accounts.FirstOrDefault(a => a.AccountName == accountname);
            return account;
        }

        public async Task<bool> AddAccount(Account account)
        {
            _dataContext.Accounts.Add(account);
            bool result = Save();
            return result;
        }

        public async void UpdateAccountLastLogin(Account account)
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
