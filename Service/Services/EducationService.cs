using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EducationService : IEducationService
    {
        private readonly AppDbContext _context;
        public EducationService()
        {
            _context = new AppDbContext();
        }
        public async Task Create(Education education)
        {
            await _context.Educations.AddAsync(education);
            await _context.SaveChangesAsync();
        }


        public async Task Delete(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            var data = _context.Educations.FirstOrDefault(e => e.Id == id);
            if (data is null) throw new ArgumentNullException("Data not found");
            _context.Educations.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Education>> GetAll()
        {
            return await _context.Educations.ToListAsync();
        }

        public async Task<Education> GetById(int id)
        {
            var data = await _context.Educations.FirstOrDefaultAsync(e => e.Id == id);
            if (data is null) throw new ArgumentNullException("Data not found");
            return data;
        }

        public async Task GetAllWithGroups()
        {
            var groupDatas = await _context.Groups.ToListAsync();
            
            var educationDatas = await _context.Educations.ToListAsync();
            foreach (var edu in educationDatas)
            {
                Console.WriteLine(edu.Name);
                Console.WriteLine($"{edu.Name}'s Groups");
                foreach (var gr in groupDatas)
                {
                    Console.WriteLine(await _context.Groups.FirstOrDefaultAsync(g=>g.EducationId==edu.Id));
                    //var datas = await _context.Groups.Where(g => g.EducationId == edu.Id).ToListAsync();
                }
            }
        }

        public async Task<List<Education>> Search(string name)
        {
            var datas = await _context.Educations.Where(e => e.Name == name).ToListAsync();
            if (datas is null) throw new ArgumentNullException("Data not found");
            return datas;
        }

        public async Task<List<Education>> SortWithCreatedDate()
        {
            var datas = await _context.Educations.OrderByDescending(e => e.CreatedTime).ToListAsync();
            return datas;
        }

        public async Task Update(Education education)
        {
            var data = _context.Educations.FirstOrDefault(g => g.Id == education.Id);
            if (data is null) throw new ArgumentNullException("Data not found");
            data.Name = education.Name;
            data.Color = education.Color;
            await _context.SaveChangesAsync();
        }

    }
}
