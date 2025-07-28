using BusinessObjects;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public List<User> GetAllUsers() => _userRepository.GetAllUsers();
        public User GetUserById(int id) => _userRepository.GetUserById(id);
        public User GetUserByEmail(string email) => _userRepository.GetUserByEmail(email);
        public void AddUser(User user) => _userRepository.AddUser(user);
        public void UpdateUser(User user) => _userRepository.UpdateUser(user);
        public void DeleteUser(int id) => _userRepository.DeleteUser(id);

        public User Authenticate(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user != null && user.Password == password) // In a real app, use proper password hashing
            {
                return user;
            }
            return null;
        }
    }
} 