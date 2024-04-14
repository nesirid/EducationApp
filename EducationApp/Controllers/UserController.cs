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
            string fullname;
            do
            {
                Console.WriteLine("Add Fullname:");
                fullname = Console.ReadLine();
                if (!await IsValidEnglishNameAsync(fullname))
                {
                    ConsoleColor.Red.WriteConsole("Invalid fullname format. Please enter a valid fullname.");
                }
            } while (!await IsValidEnglishNameAsync(fullname));

            string username;
            do
            {
                Console.WriteLine("Add Username:");
                username = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(username) || !username.All(char.IsLetter) || username.Length < 2 || username.Length > 50)
                {
                    ConsoleColor.Red.WriteConsole("Invalid username format. Please enter a valid username.");
                }
            } while (string.IsNullOrWhiteSpace(username) || !username.All(char.IsLetter) || username.Length < 2 || username.Length > 50);


            string email;
            do
            {
                Console.WriteLine("Add Email:");
                email = Console.ReadLine();
                if (!IsValidEmail(email))
                {
                    ConsoleColor.Red.WriteConsole("Invalid email format. Please enter a valid email.");
                }
            } while (!IsValidEmail(email));


            string password;
            do
            {
                Console.WriteLine("Add Password:");
                password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(password) || password.Length < 6 || password.Length > 50 || !ContainsSpecialCharacters(password) || !ContainsDigits(password))
                {
                    ConsoleColor.Red.WriteConsole("Invalid password format. Please enter a valid password (at least 6 characters, including special characters and digits).");
                }
            } while (string.IsNullOrWhiteSpace(password) || password.Length < 6 || password.Length > 50 || !ContainsSpecialCharacters(password) || !ContainsDigits(password));

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
        private async Task<bool> IsValidEnglishNameAsync(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length < 2 || str.Length > 50)
            {
                return false;
            }

            foreach (char c in str)
            {
                if (!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' '))
                {
                    return false;
                }
            }

            return true;
        }
        private bool ContainsSpecialCharacters(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ContainsDigits(string str)
        {
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
    }

    }

