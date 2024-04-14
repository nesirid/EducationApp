using Domain.Models;

namespace Service.Services.Interfaces
{
    public interface IUserService
    {
        Task Register(User user);
        bool Login(User user);

    }
}
