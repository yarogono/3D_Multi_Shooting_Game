using AccountServer.Entities;

namespace AccountServer.Repository.Contract
{
    public interface IOauthRepository
    {
        bool AddAccountOauth(Oauth oauth, Account account);
    }
}
