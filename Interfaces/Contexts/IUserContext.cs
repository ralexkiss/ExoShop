﻿using Models.DataModels;

namespace Interfaces.Contexts
{
    /// <summary>
    /// Defines functionality for a user context.
    /// </summary>
    public interface IUserContext : IContext<User>
    {
        User GetUserByid(int id);
        User Login(string email, string password);
        void Register(User user);
        void AddToWishList(Product product, User user);
        void RemoveFromWishList(Product product, User user);
    }
}