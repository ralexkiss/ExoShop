using Models.DataModels;

namespace Interfaces.Repositories
{
    /// <summary>
    /// Defines functionality for a user repository.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        User AuthenticateUser(string email, string password);
    }
}