using Data;
using Data.Repositories;
using Interfaces.Contexts;
using Interfaces.Logic;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Logic.LogicObjects
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository UserRepository;

        public UserLogic(IUserContext context)
        {
            UserRepository = new UserRepository(context);
        }

        public User AuthenticateUser(string email, string password)
        {
            return UserRepository.AuthenticateUser(email, new Hasher().GetSha256FromString(password));
        }

        public void Delete(int id)
        {
            UserRepository.Delete(id);
        }

        public List<User> GetAll()
        {
            return UserRepository.GetAll();
        }

        public User GetByid(int id)
        {
            return UserRepository.GetById(id);
        }

        public void Insert(User user)
        {
            user.Password = new Hasher().GetSha256FromString(user.Password);
            UserRepository.Insert(user);
        }

        public void Update(User user)
        {
            UserRepository.Update(user);
        }
    }
}