using Repository.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Service.Services.Interfaces;
using Service.Helpers.Extentions;


namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _context;
        private int count = 1;
        public GroupService()
        {
            _context = new AppDbContext();
        }

        public async Task Create(Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            var data = _context.Groups.FirstOrDefault(g => g.Id == id);
            if (data is null) throw new ArgumentNullException("Data not found");
            _context.Groups.Remove(data);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Group>> GetAllAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public List<Group> GetAllForMethods()
        {
            return  _context.Groups.ToList();
        }
        public async Task<List<Group>> FilterByEducationName(string eduName)
        {
            var datas = _context.Groups.Where(g => g.Education.Name == eduName).ToList();
            if (datas is null) throw new ArgumentNullException("Data not found");
            return datas;
        }
        public async Task<List<Group>> GetAllWithEducationId(int id)
        {
            var datas = _context.Groups.Where(g => g.EducationId == id).ToList();
            if (datas is null) throw new ArgumentNullException("Data not found");
            return datas;
        }

        public async Task<Group> GetById(int id)
        {
            var data = _context.Groups.FirstOrDefault(g => g.Id == id);
            if (data is null) throw new ArgumentNullException("Data not found");
            return data;
        }

        public async Task Update(Group group, int id)
        {
            //var data = _context.Groups.FirstOrDefault(g => g.Id == group.Id);
            //if (data is null) throw new ArgumentNullException("Data not found");
            //data.Name = group.Name;
            //data.Capacity = group.Capacity;
            //data.EducationId = group.EducationId;
            //data.Education = data.Education;
            //await _context.SaveChangesAsync();
            var data = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (data is null) throw new ArgumentNullException("Data not found");
            data.Name = group.Name;
            data.Capacity = group.Capacity;
            data.CreatedTime = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Group>> Search(string name)
        {
            var datas = await _context.Groups.Where(e => e.Name.Contains(name)).ToListAsync();
            if (datas.Count == 0)
            {
                ConsoleColor.Red.WriteConsole("Data not found");
            }
            return datas;
        }

        public async Task<List<Group>> SortWithCapacity()
        {
            var datas = await _context.Groups.OrderByDescending(g => g.Capacity).ToListAsync();
            return datas;
        }
    }
}
