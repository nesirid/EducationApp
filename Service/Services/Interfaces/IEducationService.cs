using Domain.Models;

namespace Service.Services.Interfaces
{
    public interface IEducationService
    {
        Task Create(Education education);
        Task Delete(int? id);
        Task Update(Education education,int id);
        Task<List<Education>> GetAll();
        List<Education> GetAllForMethods();//For Controller
        Task<Education> GetById(int id);
        Task<List<Education>> Search(string name);
        Task GetAllWithGroups();
        Task<List<Education>> SortWithCreatedDate();

    }
}
