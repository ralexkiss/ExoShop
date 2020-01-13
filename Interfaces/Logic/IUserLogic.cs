

using Models.DataModels;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines functionality for a user logic class.
    /// </summary>
    public interface IUserLogic
    {
        User GetUserByid(int id);
        User Login(string email, string password);
        void Register(User user);
        void AddToWishList(Product product, User user);
        void RemoveFromWishList(Product product, User user);

    }
}