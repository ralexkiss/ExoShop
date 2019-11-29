

using Models.DataModels;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines functionality for a user logic class.
    /// </summary>
    public interface IUserLogic : ILogic<User>
    {
        User AuthenticateUser(string email, string password);
    }
}