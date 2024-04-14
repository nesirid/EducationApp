using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService()
        {
            _context = new AppDbContext();
        }
        public async Task<bool> Login(User user)
        {
            if (user == null) Console.WriteLine("User Not Found!!!");
            User existUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);
            if (existUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task Register(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
