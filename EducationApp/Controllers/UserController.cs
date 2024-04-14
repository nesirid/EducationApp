using Service.Services.Interfaces;
using Service.Services;
using Domain.Models;
using Service.Helpers.Extentions;
using System.Text.RegularExpressions;

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
        Login: Console.WriteLine("Enter Username :");
            string username = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(username))
            {
                ConsoleColor.Red.WriteConsole("Invalid username format. Please enter a valid username.");
                goto Login;
            }

            Console.WriteLine("Enter Password :");
            string password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password))
            {
                ConsoleColor.Red.WriteConsole("Invalid password format. Please enter a valid password.");
                goto Login;
            }

            User loginUser = new User
            {
                Username = username,
                Password = password,
            };
            bool existUser = _userService.Login(loginUser);
            if (existUser)
            {
                ConsoleColor.Green.WriteConsole("Login Succesfully");
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Username or Password is wrong!!!");
                goto Login;
            }
        }

        public async Task Register()
        {
            Console.WriteLine("Add Fullname:");
            string fullname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(fullname) || !fullname.All(char.IsLetter))
            {
                ConsoleColor.Red.WriteConsole("Invalid fullname format. Please enter a valid fullname.");
                return;
            }

            Console.WriteLine("Add Username:");
            string username = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(username))
            {
                ConsoleColor.Red.WriteConsole("Invalid username format. Please enter a valid username.");
                return;
            }

            Console.WriteLine("Add Email:");
            string email = Console.ReadLine();
            if (!IsValidEmail(email))
            {
                ConsoleColor.Red.WriteConsole("Invalid email format. Please enter a valid email.");
                return;
            }

            Console.WriteLine("Add Password:");
            string password = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(password))
            {
                ConsoleColor.Red.WriteConsole("Invalid password format. Please enter a valid password.");
                return;
            }

            User newUser = new User
            {
                FullName = fullname,
                Username = username,
                Email = email,
                Password = password,
                CreatedTime = DateTime.Now
            };

            await _userService.Register(newUser);
            ConsoleColor.Green.WriteConsole("Registration successful");
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

    }
}
