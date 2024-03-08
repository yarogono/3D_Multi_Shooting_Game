using AccountServer.Entities;

namespace AccountServer.Repository.Contract
{
    public interface IOauthRepository : IDisposable
    {
        bool AddAccountOauth(Oauth oauth, Account account);
    }
}
