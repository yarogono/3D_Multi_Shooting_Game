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

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public bool AddAccountOauth(Oauth oauth, Account account)
        {
            _dataContext.Oauths.Add(oauth);

            return Save();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() >= 0 ? true : false;
        }
    }
}
