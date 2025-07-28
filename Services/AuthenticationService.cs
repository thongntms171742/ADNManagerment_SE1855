using BusinessObjects;
using System;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;

        public AuthenticationService()
        {
            _userService = new UserService();
        }

        public User Login(string email, string password)
        {
            try
            {
                return _userService.Authenticate(email, password);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Register(User user)
        {
            try
            {
                if (IsEmailExists(user.Email))
                {
                    throw new Exception("Email already exists");
                }

                user.CreatedDate = DateTime.Now;
                _userService.AddUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsEmailExists(string email)
        {
            try
            {
                return _userService.GetUserByEmail(email) != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
} 