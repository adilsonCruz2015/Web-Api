
using OAuthServer.Domain.Entities;
namespace OAuthServer.Domain.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        User Autenticate(string username, string password);
    }
}
