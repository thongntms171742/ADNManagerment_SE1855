using BusinessObjects;
using System.Collections.Generic;

namespace Services
{
    public interface IAuthenticationService
    {
        User Login(string email, string password);
        void Register(User user);
        bool IsEmailExists(string email);
    }
} 