using Domain.Models;
using Service.Services;
using Service.Services.Interfaces;
using System.Data;
using System.Reflection.Emit;

namespace EducationApp.Controllers
{
    internal class GroupController
    {
        private readonly IGroupService _groupService;
        public GroupController()
        {
            _groupService = new GroupService();
        }

        public async Task Create()
        {
            Console.WriteLine("Add Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Add Capasity:");
            //string capStr=Console.ReadLine();
            int capasity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Add Education Id:");
            int educationId = Convert.ToInt32(Console.ReadLine());


            await _groupService.Create(new Group { Name = name, Capacity = capasity, EducationId = educationId, CreatedTime = DateTime.UtcNow });
        }
        public async Task Delete()
        {
            Console.WriteLine("Enter Group Id");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                await _groupService.Delete(id);
                Console.WriteLine("Data Deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                //string capStr=Console.ReadLine();
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task GetAllAsync()
        {
            var datas = await _groupService.GetAllAsync();
            foreach (var item in datas)
            {
                string data = $"Name : {item.Name}, Capasity : {item.Capacity}, Education : {item.Education.Name}";
                Console.WriteLine(data);
            }
        }
        public async Task GetByIdAsync()
        {
            Console.WriteLine("Write Group Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            try
            {
                var data = await _groupService.GetById(id);
                string result = $"Name : {data.Name}, Capasity : {data.Capacity}, Education : {data.Education.Name}";
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public async Task Search()
        {
            Console.WriteLine("Write Group Name:");
            string name = Console.ReadLine();
            var datas = await _groupService.Search(name);

            foreach (var item in datas)
            {
                string data = $"Name : {item.Name}, Capasity : {item.Capacity}, Education : {item.Education.Name}";
                Console.WriteLine(data);
            }
        }
        public async Task FilterByEducationName()
        {
            Console.WriteLine("Enter Education Name:");
            string eduName = Console.ReadLine();
            var datas = await _groupService.FilterByEducationName(eduName);
            foreach (var item in datas)
            {
                string data = $"Name : {item.Name}, Capasity : {item.Capacity}, Education : {item.Education.Name}";
                Console.WriteLine(data);
            }
        }
        public async Task GetAllWithEducationId()
        {
            Console.WriteLine("Enter Id:");
            int id = Convert.ToInt32(Console.ReadLine());
            var datas = await _groupService.GetAllWithEducationId(id);
            foreach (var item in datas)
            {
                string data = $"Name : {item.Name}, Capasity : {item.Capacity}, Education : {item.Education.Name}";
                Console.WriteLine(data);
            }
        }
        public async Task SortWithCapacity()
        {
            Console.WriteLine("Sorted Groups:");
            var datas = await _groupService.SortWithCapacity();
            foreach (var item in datas)
            {
                string data = $"Name : {item.Name}, Capasity : {item.Capacity}, Education : {item.Education.Name}";
                Console.WriteLine(data);
            }
        }


    }
}
