using Service.Services.Interfaces;
using Service.Services;
using Domain.Models;

namespace EducationApp.Controllers
{
    internal class UserController
    {
        private readonly IUserService _userService;
        public UserController()
        {
            _userService = new UserService();
        }

        public async Task Login()
        {
            Console.WriteLine("Enter Username :");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Password :");
            string password = Console.ReadLine();

            User loginUser = new User
            {
                Username = username,
                Password = password,
            };
            bool existUser = await _userService.Login(loginUser);
            if(existUser)
            {
                Console.WriteLine("Login Succesfully");
            }
            else
            {
                Console.WriteLine("Username or Password is wrong!!!");
            }
        }

        public async Task Register()
        {
            Console.WriteLine("Add Fullname :");
            string fullname = Console.ReadLine();
            
            Console.WriteLine("Add Username :");
            string username = Console.ReadLine();

            Console.WriteLine("Add Email :");
            string email = Console.ReadLine();

            Console.WriteLine("Add Password :");
            string password = Console.ReadLine();

            User newUser = new User
            {
                FullName = fullname,
                Username = username,
                Email = email,
                Password = password,
                CreatedTime = DateTime.UtcNow
            };

            await _userService.Register(newUser);
            Console.WriteLine("Register is successfully");
        }

    }
}
