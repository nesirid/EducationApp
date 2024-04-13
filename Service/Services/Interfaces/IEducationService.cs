using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEducationService
    {
        Task Create(Education education);
        Task Delete(int? id);
        Task Update(Education education);
        Task<List<Education>> GetAll();
        Task<Education> GetById(int id);
        Task<List<Education>> Search(string name);
        Task GetAllWithGroups();
        Task<List<Education>> SortWithCreatedDate();

    }
}
