using Domain.Models;

namespace Service.Services.Interfaces
{
    public interface IUserService
    {
        Task Register(User user);
        Task<bool> Login(User user);

    }
}
