using Domain.Models;
using Service.Helpers.Extentions;
using Service.Services;
using Service.Services.Interfaces;

namespace EducationApp.Controllers
{
    internal class GroupController
    {
        private readonly IGroupService _groupService;
        private readonly IEducationService _educationService;

        public GroupController()
        {
            _groupService = new GroupService();
            _educationService = new EducationService();
        }

        public async Task Create()
        {
        Group:
            Console.WriteLine("Add Name:");
            string name = Console.ReadLine();
            var datas = await _groupService.GetAllAsync();
            foreach (var item in datas)
            {
                if (item.Name == name)
                {
                    ConsoleColor.Red.WriteConsole("Already exist group name!!!");
                    goto Group;
                }
            }
            if (string.IsNullOrWhiteSpace(name) || !name.All(char.IsLetter))
            {
                ConsoleColor.Red.WriteConsole("Group Name format is wrong!!!");
                goto Group;
            }

            int capacity;
            bool isValidCapacity = false;
            do
            {
                Console.WriteLine("Add Capacity:");
                string capacityInput = Console.ReadLine();

                if (int.TryParse(capacityInput, out capacity))
                {
                    isValidCapacity = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for the capacity.");
                }
            } while (!isValidCapacity);

            var eduDatas = _educationService.GetAllForMethods();
            foreach (var item in eduDatas)
            {
                string data = $"Id : {item.Id}  Name : {item.Name}";
                Console.WriteLine(data);
            }

            int educationId;
            bool isValidEducationId = false;
            do
            {
                Console.WriteLine("Add Education Id:");
                string educationIdInput = Console.ReadLine();

                if (int.TryParse(educationIdInput, out educationId))
                {
                    //var eduDatas = _educationService.GetAllForMethods();
                    if (eduDatas.Any(edu => edu.Id == educationId))
                    {
                        isValidEducationId = true;
                    }
                    else
                    {
                        ConsoleColor.Red.WriteConsole("Invalid Education Id. Please enter an existing Education Id.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for the Education Id.");
                }
            } while (!isValidEducationId);

            await _groupService.Create(new Group { Name = name, Capacity = capacity, EducationId = educationId, CreatedTime = DateTime.Now });
            ConsoleColor.Green.WriteConsole("Created Successfully");
        }
        public async Task Delete()
        {
            var datas = _groupService.GetAllForMethods();
            foreach (var item in datas)
            {
                string data = $"Id : {item.Id}, Name: {item.Name}";
                Console.WriteLine(data);
            }
            int groupId;
            bool isValidId = false;
            
            do
            {
                
                Console.WriteLine("Enter Group Id:");
                string idInput = Console.ReadLine();

                if (int.TryParse(idInput, out groupId))
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
                Console.WriteLine("Are you sure you want to delete this group? (YES/NO)");
                confirmation = Console.ReadLine().ToUpper();
                if (confirmation == "YES")
                {
                    try
                    {
                        await _groupService.Delete(groupId);
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
            //try
            //{
            //    await _groupService.Delete(id);
            //    ConsoleColor.Green.WriteConsole("Data Deleted");
            //}
            //catch (Exception ex)
            //{
            //    ConsoleColor.Red.WriteConsole(ex.Message);
            //}
        }
        public async Task Update()
        {
            Console.WriteLine("Enter Group Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                var data = await _groupService.GetById(id);
                Console.WriteLine("Enter Name:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Capasity:");
                int capasity = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Education Id:");
                int educationId = Convert.ToInt32(Console.ReadLine());

                var updatedGroup = new Group
                {
                    Id = data.Id,
                    Name = name,
                    Capacity = capasity,
                    EducationId = educationId
                };
                await _groupService.Update(updatedGroup);
                ConsoleColor.Green.WriteConsole("Updated Succesfully");
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }
        }
        public async Task GetAllAsync()
        {
            var eduDatas = _educationService.GetAllForMethods();
            var datas =  _groupService.GetAllForMethods();
            foreach (var item in datas)
            {
                foreach (var edu in eduDatas)
                {
                    if (item.EducationId == edu.Id)
                    {
                        string data = $"Id : {item.Id}, Name : {item.Name}, Capasity : {item.Capacity}, Education : {edu.Name}";
                        Console.WriteLine(data);
                    }
                }
            }
        }
        public async Task GetByIdAsync()
        {
            Console.WriteLine("Write Group Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                var eduDatas = await _educationService.GetAll();
                var data = await _groupService.GetById(id);
                foreach (var item in eduDatas)
                {
                    if (item.Id == data.EducationId)
                    {
                        string result = $"Id : {item.Id}, Name : {data.Name}, Capasity : {data.Capacity}, Education : {item.Name}";
                        Console.WriteLine(result);
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleColor.Red.WriteConsole(ex.Message);
            }

        }
        public async Task Search()
        {
            Console.WriteLine("Enter input to search");
            string name = Console.ReadLine();

            var datas = await _groupService.Search(name);

            foreach (var item in datas)
            {
                string data = $"Id : {item.Id}, Name : {item.Name}, Color :  {item.Capacity} , Created Date :  {item.CreatedTime}";
                Console.WriteLine(data);
            }
        }
        public async Task FilterByEducationName()
        {
            Console.WriteLine("Enter Education Name:");
            string eduName = Console.ReadLine();
            var datas = await _groupService.FilterByEducationName(eduName);
            var eduDatas = _educationService.GetAllForMethods();
            foreach (var item in datas)
            {
                foreach (var edu in eduDatas)
                {
                    if (item.EducationId == edu.Id)
                    {
                        string data = $"Id : {item.Id}, Name : {item.Name}, Capasity : {item.Capacity}, Education : {edu.Name}";
                        Console.WriteLine(data);
                    }
                }
            }
        }
        public async Task GetAllWithEducationId()
        {
            Console.WriteLine("Enter Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            var datas = await _groupService.GetAllWithEducationId(id);
            foreach (var item in datas)
            {
                string data = $"Id : {item.Id}, Name : {item.Name}, Capasity : {item.Capacity}, Education : {item.EducationId}";
                Console.WriteLine(data);
            }
        }
        public async Task SortWithCapacity()
        {
            Console.WriteLine("Sorted Groups:");
            var eduDatas = _educationService.GetAllForMethods();
            var datas = await _groupService.SortWithCapacity();
            foreach (var item in datas)
            {
                foreach (var edu in eduDatas)
                {
                    if (item.EducationId == edu.Id)
                    {
                        string data = $"Id : {item.Id}, Name : {item.Name}, Capasity : {item.Capacity}, Education : {edu.Name}";
                        Console.WriteLine(data);
                    }
                }
            }
        }


    }
}
