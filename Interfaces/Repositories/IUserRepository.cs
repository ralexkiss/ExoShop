using Models.DataModels;

namespace Interfaces.Repositories
{
    /// <summary>
    /// Defines functionality for a user repository.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByid(int id);
        User Login(string email, string password);
        void Register(User user);
        void AddToWishList(Product product, User user);
        void RemoveFromWishList(Product product, User user);
    }
}