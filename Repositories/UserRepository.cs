using BusinessObjects;
using DataAccessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _userDAO;

        public UserRepository()
        {
            _userDAO = new UserDAO();
        }

        public List<User> GetAllUsers() => _userDAO.GetAllUsers();
        public User GetUserById(int id) => _userDAO.GetUserById(id);
        public User GetUserByEmail(string email) => _userDAO.GetUserByEmail(email);
        public void AddUser(User user) => _userDAO.AddUser(user);
        public void UpdateUser(User user) => _userDAO.UpdateUser(user);
        public void DeleteUser(int id) => _userDAO.DeleteUser(id);
    }
} 