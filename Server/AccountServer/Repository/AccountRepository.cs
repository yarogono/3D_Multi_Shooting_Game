using AccountServer.DB;
using AccountServer.Model;
using AccountServer.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace AccountServer.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _dataContext;

        public AccountRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Account> GetAccountByAccountnameAsync(string accountname)
        {
            return await _dataContext.Accounts.FirstOrDefaultAsync(a => a.AccountName == accountname);
        }

        public async Task<bool> AddAccount(Account account)
        {
            await _dataContext.Accounts.AddAsync(account);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
