using AccountServer.Entities;

namespace AccountServer.Repository.Contract
{
    public interface IOauthRepository
    {
        Task<Oauth> AddAccountOauth(Oauth oauth);
    }
}
