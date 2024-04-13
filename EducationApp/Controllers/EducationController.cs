using Service.Services.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using System.Xml.Linq;

namespace EducationApp.Controllers
{
    internal class EducationController
    {
        private readonly IEducationService _educationService;
        public EducationController()
        {
            _educationService = new EducationService();
        }

        public async Task Create()
        {
            Console.WriteLine("Add Education Name:");
            string eduName = Console.ReadLine();

            Console.WriteLine("Add Education Color:");
            string eduColor = Console.ReadLine();

            await _educationService.Create(new Education { Name = eduName, Color = eduColor, CreatedTime = DateTime.UtcNow });

        }
        public async Task Delete()
        {
            Console.WriteLine("Enter Education Id");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                await _educationService.Delete(id);
                Console.WriteLine("Data Deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task GetAll()
        {
            var datas = await _educationService.GetAll();
            foreach (var item in datas)
            {
                string data = $"Name : {item.Name}, Color : {item.Color}, Created Date : {item.CreatedTime}";
                Console.WriteLine(data);
            }
        }
        public async Task GetById()
        {
            Console.WriteLine("Write Education Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                var data = await _educationService.GetById(id);
                string result = $"Name : {data.Name}, Color : {data.Color}, Created Date : {data.CreatedTime}";
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task GetAllWithGroups()
        {
            await _educationService.GetAllWithGroups();
        }
        public async Task Search()
        {
            Console.WriteLine("Enter input to search");
            string input = Console.ReadLine();

            var datas = await _educationService.Search(input);

            foreach (var item in datas)
            {
                string data = $"Name : {item.Name}, Color :  {item.Color} , Created Date :  {item.CreatedTime}";
                Console.WriteLine(data);
            }
        }
        public async Task SortWithCreatedDate()
        {
            var datas = await _educationService.SortWithCreatedDate();
            foreach (var item in datas)
            {
                string data = $"Name : {item.Name}, Color : {item.Color}, Created Date : {item.CreatedTime}";
                Console.WriteLine(data);
            }
        }
        public async Task Update()
        {
            Console.WriteLine("Enter Education Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                var data = await _educationService.GetById(id);
                Console.WriteLine("Enter Name:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Color:");
                //string capStr=Console.ReadLine();
                string color = Console.ReadLine();

                var updatedEducation = new Education
                {
                    Id = data.Id,
                    Name = name,
                    Color = color
                };
                await _educationService.Update(updatedEducation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
