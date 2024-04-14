using EducationApp.Controllers;
using Service.Helpers.Extentions;

EducationController educationController = new();
GroupController groupController = new();
UserController userController = new();

Console.WriteLine("Do You Have Acount? \n y / n");
char choose = Convert.ToChar(Console.ReadLine());
if (choose == 'y')
{
    Console.WriteLine("Login");
    userController.Login();
}
else if (choose == 'n')
{
    Console.WriteLine("Register");
    userController.Register();  
}
else
{
    Console.WriteLine("You have to choose 'y' or 'n'");
}

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
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 11:
                break;
            case 10:
                break;
            case 9:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            default:
                ConsoleColor.Red.WriteConsole("Operation is wrong, please choose again");
                goto Operation;
        }
    }
    else
    {
        ConsoleColor.Red.WriteConsole("Operation format is wrong, please add operation again");
        goto Operation;
    }
}

static void GetMenues()
{
    ConsoleColor.Cyan.WriteConsole("Choose one operation : " +
        "\n 1 - " +
        "\n 2 - " +
        "\n 3 - " +
        "\n 4 - " +
        "\n 5 - " +
        "\n 6 - " +
        "\n 7 - " +
        "\n 8 - " +
        "\n 9 - " +
        "\n 10 - " +
        "\n 11 - " +
        "\n 12 - " +
        "\n 13 - " +
        "\n 14 - " +
        "\n 15 - ");
}

