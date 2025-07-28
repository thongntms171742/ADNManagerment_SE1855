using BusinessObjects;
using System.Collections.Generic;

namespace Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        User Authenticate(string email, string password);
    }
} 