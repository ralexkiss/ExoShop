using Interfaces.Contexts;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext Context;

        public UserRepository(IUserContext context)
        {
            Context = context;
        }

        public User AuthenticateUser(string email, string password)
        {
            return Context.AuthenticatUser(email, password);
        }

        public void Delete(int id)
        {
            Context.Delete(id);
        }

        public List<User> GetAll()
        {
            return Context.GetAll();
        }

        public User GetById(int id)
        {
            return Context.GetById(id);
        }

        public void Insert(User user)
        {
            Context.Insert(user);
        }

        public void Update(User user)
        {
            Context.Update(user);
        }
    }
}