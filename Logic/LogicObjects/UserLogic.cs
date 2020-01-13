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

        public User Login(string email, string password)
        {
            return UserRepository.Login(email, new Hasher().GetSha256FromString(password));
        }

        public void Register(User user)
        {
            user.Password = new Hasher().GetSha256FromString(user.Password);
            UserRepository.Insert(user);
        }

        public List<User> GetAll()
        {
            return UserRepository.GetAll();
        }

        public User GetUserByid(int id)
        {
            return UserRepository.GetById(id);
        }
    }
}