using AccountServer.DB;
using AccountServer.Entities;
using AccountServer.Repository.Contract;

namespace AccountServer.Repository
{
    public class OauthRepository : IOauthRepository
    {
        private readonly DataContext _dataContext;

        public OauthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddAccountOauth(Oauth oauth, Account account)
        {
            await _dataContext.Accounts.AddAsync(account);
            await _dataContext.Oauths.AddAsync(oauth);

            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
