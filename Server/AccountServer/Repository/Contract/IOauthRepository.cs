using AccountServer.Entities;

namespace AccountServer.Repository.Contract
{
    public interface IOauthRepository
    {
        Task<bool> AddAccountOauth(Oauth oauth, Account account);
    }
}
