using AccountServer.Entities;

namespace AccountServer.Repository.Contract
{
    public interface IOauthRepository : IDisposable
    {
        public Task<bool> AddAccountOauth(Oauth oauth, Account account);
    }
}
