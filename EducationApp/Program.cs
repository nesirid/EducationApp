using Domain.Models;
using EducationApp.Controllers;
using Service.Helpers.Extentions;

EducationController educationController = new();
GroupController groupController = new();
UserController userController = new();

char choose;
do
{
    ConsoleColor.Yellow.WriteConsole("Do You Have Account? \n y / n");
    choose = Console.ReadKey().KeyChar;
    Console.WriteLine();

    if (choose == 'y')
    {
        Console.WriteLine("Login");
        userController.Login();

        while (true)
        {
            GetMenues();
        Operation: string? operationStr = Console.ReadLine();

            int operation;

            bool isCorrectOperationFormat = int.TryParse(operationStr, out operation);
            if (isCorrectOperationFormat)
            {
                switch (operation)
                {
                    case 1:
                        educationController.Create();
                        break;
                    case 2:
                        educationController.Delete();
                        break;
                    case 3:
                        educationController.Update();
                        break;
                    case 4:
                        educationController.GetById();
                        break;
                    case 5:
                        educationController.GetAllWithGroups();
                        break;
                    case 6:
                        educationController.Search();
                        break;
                    case 7:
                        educationController.SortWithCreatedDate();
                        break;
                    case 8:
                        educationController.GetAll();
                        break;
                    case 9:
                        groupController.Create();
                        break;
                    case 10:
                        groupController.Delete();
                        break;
                    case 11:
                        groupController.Update();
                        break;
                    case 12:
                        groupController.GetAllAsync();
                        break;
                    case 13:
                        groupController.GetByIdAsync();
                        break;
                    case 14:
                        groupController.Search();
                        break;
                    case 15:
                        groupController.FilterByEducationName();
                        break;
                    case 16:
                        groupController.GetAllWithEducationId();
                        break;
                    case 17:
                        groupController.SortWithCapacity();
                        break;
                    default:
                        ConsoleColor.Red.WriteConsole("You can choose 1-17!!!");
                        goto Operation;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Operation format is wrong, please add operation again");
                goto Operation;
            }
        }

        //break;
    }
    else if (choose == 'n')
    {
        Console.WriteLine("Register");
        userController.Register();
        userController.Login();
        while (true)
        {
            GetMenues();
        Operation: string? operationStr = Console.ReadLine();

            int operation;

            bool isCorrectOperationFormat = int.TryParse(operationStr, out operation);
            if (isCorrectOperationFormat)
            {
                switch (operation)
                {
                    case 1:
                        educationController.Create();
                        break;
                    case 2:
                        educationController.Delete();
                        break;
                    case 3:
                        educationController.Update();
                        break;
                    case 4:
                        educationController.GetById();
                        break;
                    case 5:
                        educationController.GetAllWithGroups();
                        break;
                    case 6:
                        educationController.Search();
                        break;
                    case 7:
                        educationController.SortWithCreatedDate();
                        break;
                    case 8:
                        educationController.GetAll();
                        break;
                    case 9:
                        groupController.Create();
                        break;
                    case 10:
                        groupController.Delete();
                        break;
                    case 11:
                        groupController.Update();
                        break;
                    case 12:
                        groupController.GetAllAsync();
                        break;
                    case 13:
                        groupController.GetByIdAsync();
                        break;
                    case 14:
                        groupController.Search();
                        break;
                    case 15:
                        groupController.FilterByEducationName();
                        break;
                    case 16:
                        groupController.GetAllWithEducationId();
                        break;
                    case 17:
                        groupController.SortWithCapacity();
                        break;
                    default:
                        ConsoleColor.Red.WriteConsole("You can choose 1-17!!!");
                        goto Operation;
                }
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Operation format is wrong, please add operation again");
                goto Operation;
            }
        }
        
    }
    else
    {
        ConsoleColor.Red.WriteConsole("You have to choose 'y' or 'n'");
    }
} while (true);




static void GetMenues()
{

    ConsoleColor.Cyan.WriteConsole(@"
╔════════════════════════════════════════════╗
║            EDUCATION MANAGEMENT            ║
╠════════════════════════════════════════════╣
║  1 - Create Education                      ║
║  2 - Delete Education                      ║
║  3 - Update Education                      ║
║  4 - GetById Education                     ║
║  5 - Get All Education With Groups         ║
║  6 - Search Education                      ║
║  7 - Sort Education With Created Date      ║
║  8 - GetAll Education                      ║
║════════════════════════════════════════════║
║              GROUPS MANAGEMENT             ║
╠════════════════════════════════════════════╣
║  9  - Create Groups                        ║
║ 10 - Delete Groups                         ║
║ 11 - Update Groups                         ║
║ 12 - Get All Groups                        ║
║ 13 - Get Groups By Id                      ║
║ 14 - Search Groups                         ║
║ 15 - Filter By Education Name              ║
║ 16 - Get All With Education Id             ║
║ 17 - Sort With Groups Capacity             ║
╚════════════════════════════════════════════╝
");

}

