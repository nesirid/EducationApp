using Domain.Models;


namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Task Create(Group group);
        Task Delete(int? id);
        Task Update(Group group);
        Task<List<Group>> Search(string name);
        Task<Group> GetById(int id);
        Task<List<Group>> GetAllAsync();
        List<Group> GetAllForMethods();//For Controller
        Task<List<Group>> FilterByEducationName(string edName);
        Task<List<Group>> GetAllWithEducationId(int id);
        Task<List<Group>> SortWithCapacity();

    }
}
