using Domain.Models;
using Repository.Data;
using Service.Helpers.Extentions;
using Service.Services;
using Service.Services.Interfaces;

namespace EducationApp.Controllers
{
    internal class GroupController
    {
        private readonly IGroupService _groupService;
        private readonly IEducationService _educationService;
        private readonly AppDbContext _context;


        public GroupController()
        {
            _groupService = new GroupService();
            _educationService = new EducationService();
            _context = new AppDbContext();
        }

        public async Task Create()
        {
            string name;
            int capacity;
            int educationId;

            bool createAnotherGroup = true;

            while (createAnotherGroup)
            {
                Console.WriteLine("Add Group Name:");
                name = Console.ReadLine();

                var datas = await _groupService.GetAllAsync();
                if (datas.Any(item => item.Name == name))
                {
                    ConsoleColor.Red.WriteConsole("Group name already exists!");
                    break;
                }

                if (string.IsNullOrWhiteSpace(name) || !name.All(char.IsLetter))
                {
                    ConsoleColor.Red.WriteConsole("Group Name format is wrong!");
                    break;
                }

                bool isValidCapacity = false;
                do
                {
                    Console.WriteLine("Add Group Capacity 1-18:");
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

                bool isValidEducationId = false;
                do
                {
                    Console.WriteLine("Add Education Id: " );
                    string educationIdInput = Console.ReadLine();

                    if (int.TryParse(educationIdInput, out educationId))
                    {
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

                // CREATE GROUP
                await _groupService.Create(new Group { Name = name, Capacity = capacity, EducationId = educationId, CreatedTime = DateTime.Now });
                ConsoleColor.Green.WriteConsole("Created Successfully");

                // ASK
                Console.WriteLine("Do you want to create another group? (y/n)");
                string response = Console.ReadLine();
                createAnotherGroup = response.ToLower() == "y";
                if (!createAnotherGroup)
                {
                    break;
                }
            }
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

                ConsoleColor.Yellow.WriteConsole("Enter Group Id:");
                string idInput = Console.ReadLine();

                if (int.TryParse(idInput, out groupId))
                {
                    isValidId = true;
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid number for the ID.");
                }
            } while (!isValidId);
            string confirmation;
            do
            {
                ConsoleColor.Yellow.WriteConsole("Are you sure you want to delete this group? (YES/NO)");
                confirmation = Console.ReadLine().ToUpper();
                if (confirmation == "YES")
                {
                    try
                    {
                        await _groupService.Delete(groupId);
                        ConsoleColor.Green.WriteConsole("Data Deleted");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                }
                else if (confirmation == "NO")
                {
                    ConsoleColor.Red.WriteConsole("Deletion cancelled.");
                    break;
                }
                else
                {
                    ConsoleColor.Red.WriteConsole("Invalid input. Please enter 'YES' or 'NO'.");
                }
            } while (true);
            
        }
        public async Task Update()
        {
            var groupDatas = _groupService.GetAllForMethods();
            foreach (var item in groupDatas)
            {
                string data = $"Id : {item.Id}  Name : {item.Name}, Color : {item.Capacity}";
                Console.WriteLine(data);
            }

            int id;
            bool isValidId = false;
            do
            {
                Console.WriteLine("Enter Group Id (a number):");
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
            //int id = Convert.ToInt32(Console.ReadLine());

            var existGro =  _context.Groups.FirstOrDefault(g => g.Id == id);
            if (existGro == null)
            {
                ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid number for the ID.");
            }
            else
            {
                try
                {
                    Console.WriteLine("Enter Name:");
                    //string name = Console.ReadLine();
                    string groName;
                    bool isValidName = false;

                    do
                    {
                       groName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(groName))
                        {
                            isValidName = true;
                        }
                        else
                        {
                            ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid name.");
                        }
                    } while (!isValidName);

                    Console.WriteLine("Enter Capasity:");
                    int capacity;
                    bool isValidCapacity = false;

                    do
                    {
                        string capacityInput = Console.ReadLine();
                        if (int.TryParse(capacityInput, out capacity))
                        {
                            isValidCapacity = true;
                        }
                        else
                        {
                            ConsoleColor.Red.WriteConsole("Invalid input. Please enter a valid number for the capacity.");
                        }
                    } while (!isValidCapacity);
                    //int capasity = Convert.ToInt32(Console.ReadLine());
                    bool isValid = false;

                    Group newGroup = new Group
                    {
                        Name = groName,
                        Capacity = capacity,
                        CreatedTime = DateTime.Now,
                    };

                    await _groupService.Update(newGroup, id);
                    ConsoleColor.Green.WriteConsole("Updated Succesfully");
                }
                catch (Exception ex)
                {
                    ConsoleColor.Red.WriteConsole(ex.Message);
                }
            }
            
        }
        public async Task GetAllAsync()
        {
            var eduDatas = _educationService.GetAllForMethods();
            var datas = _groupService.GetAllForMethods();
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
                        string result = $"Id : {data.Id}, Name : {data.Name}, Capasity : {data.Capacity}, Education : {item.Name}";
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
