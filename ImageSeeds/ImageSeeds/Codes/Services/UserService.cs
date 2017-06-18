using System.Collections.Generic;
using ImageSeeds.Codes.Entities;
using ImageSeeds.Codes.Repositories;

namespace ImageSeeds.Codes.Services
{
    public class UserService
    {
        private readonly UserRepository _repository = new UserRepository();

        public List<User> Listuser()
        {
            return _repository.Listuser();
        }

        public User GetUserById(long id)
        {
            return _repository.GetUserById(id);
        }

        public long InsertUser(User obj)
        {
            return _repository.InsertUser(obj);
        }

        public long UpdateUser(User obj)
        {
            return _repository.UpdateUser(obj);
        }

        public void DeleteUser(int id)
        {
            _repository.DeleteUser(id);
        }
    }
}