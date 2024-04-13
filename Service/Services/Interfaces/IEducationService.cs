using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    internal interface IEducationService
    {
        Task Create(Education education);
        Task Delete(int? id);
        Task Update(Education education);
        Task<List<Education>> GetAll();
        Task<Group> GetById(int id);
        Task<List<Group>> Search(string name);
        Task<List<Group>> GettAllWithGroups();
        Task<List<Group>> SortWithCreatedDate();

    }
}
