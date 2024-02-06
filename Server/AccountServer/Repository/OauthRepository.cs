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

        public async Task<Oauth> AddAccountOauth(Oauth oauth)
        {
            await _dataContext.Oauths.AddAsync(oauth);

            return null;
        }
    }
}
