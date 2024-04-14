using Service.Services.Interfaces;
using Service.Services;
using Domain.Models;
using Repository.Data;
using Service.Helpers.Extentions;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace EducationApp.Controllers
{
    internal class EducationController
    {
        private readonly IEducationService _educationService;
        private readonly IGroupService _groupService;
        private readonly AppDbContext _context;


        public EducationController()
        {
            _educationService = new EducationService();
            _groupService = new GroupService();
            _context = new AppDbContext();
        }

        public async Task Create()
        {
        Edu: Console.WriteLine("Add Education Name:");
            string eduName = Console.ReadLine();
            var datas = await _educationService.GetAll();
            foreach (var item in datas)
            {
                if (item.Name == eduName)
                {
                    ConsoleColor.Red.WriteConsole("Already exist education name!!!");
                    goto Edu;
                }
            }
            
            Console.WriteLine("Add Education Color:");
            string eduColor = Console.ReadLine();
            

            if (string.IsNullOrWhiteSpace(eduName) || !eduName.All(char.IsLetter)
                || string.IsNullOrWhiteSpace(eduColor) || !eduColor.All(char.IsLetter))
            {
                ConsoleColor.Red.WriteConsole("Education Color Name format is wrong!!!");
                goto Edu;
            }

            _educationService.Create(new Education { Name = eduName, Color = eduColor, CreatedTime = DateTime.Now });
            ConsoleColor.Green.WriteConsole("Created Education Successfuly");
            
        }


        public async Task Delete()
        {
            var eduDatas = _educationService.GetAllForMethods();
            foreach (var item in eduDatas)
            {
                string data = $"Id : {item.Id}  Name : {item.Name}";
                Console.WriteLine(data);
            }
            int id;
            bool isValidId = false;
            do
            {
                Console.WriteLine("Enter Education Id (a number):");
                string idInput = Console.ReadLine();

                if (int.TryParse(idInput, out id))
                {
                    isValidId = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for the ID.");
                }
            } while (!isValidId);
            string confirmation;
            do
            {
                Console.WriteLine("Are you sure you want to delete this education? (YES/NO)");
                confirmation = Console.ReadLine().ToUpper();
                if (confirmation == "YES")
                {
                    try
                    {
                        await _educationService.Delete(id);
                        Console.WriteLine("Data Deleted");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                }
                else if (confirmation == "NO")
                {
                    Console.WriteLine("Deletion cancelled.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'YES' or 'NO'.");
                }
            } while (true);
        }
        public async Task GetAll()
        {
            var datas = _educationService.GetAllForMethods();
            foreach (var item in datas)
            {
                string data = $"Id : {item.Id} Name : {item.Name}, Color : {item.Color}, Created Date : {item.CreatedTime}";
                Console.WriteLine(data);
            }
        }
        public async Task GetById()
        {
            int id;
            bool isValidId = false;
            do
            {
                Console.WriteLine("Write Education Id:");
                string idInput = Console.ReadLine();

                if (int.TryParse(idInput, out id))
                {
                    isValidId = true;
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid number for the ID.");
                }
            } while (!isValidId);

            try
            {
                var data = await _educationService.GetById(id);
                string result = $"Id : {data.Id} Name : {data.Name}, Color : {data.Color}, Created Date : {data.CreatedTime}";
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
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
            var eduDatas = _educationService.GetAllForMethods();
            foreach (var item in eduDatas)
            {
                string data = $"Id : {item.Id}  Name : {item.Name}, Color : {item.Color}";
                Console.WriteLine(data);
            }
            int id;
            bool isValidId = false;
            do
            {
                Console.WriteLine("Enter Education Id (a number):");
                string idInput = Console.ReadLine();


                if (int.TryParse(idInput, out id))
                {
                    isValidId = true;
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid number for the ID.");
                }
            } while (!isValidId);
            var existEdu = await _context.Educations.FirstOrDefaultAsync(g => g.Id == id);
            if (existEdu == null)
            {
                ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid number for the ID.");
            }
            else
            {
                try
                {
                    Console.WriteLine("Enter Name:");
                    string name = Console.ReadLine();

                    Console.WriteLine("Enter Color:");
                    string color = Console.ReadLine();

                    Education newEdu = new Education
                    {
                        Name = name,
                        Color = color
                    };

                    await _educationService.Update(newEdu, id);
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
        }
        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.All(char.IsLetter);
        }

    }

}
