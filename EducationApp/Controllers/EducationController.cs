using Service.Services.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

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

    }
}
