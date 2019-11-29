using Models.DataModels;

namespace Interfaces.Contexts
{
    /// <summary>
    /// Defines functionality for a user context.
    /// </summary>
    public interface IUserContext : IContext<User>
    {
        User AuthenticatUser(string email, string password);
    }
}