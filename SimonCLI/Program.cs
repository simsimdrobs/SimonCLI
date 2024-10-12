using Dapper;
using Microsoft.Data.Sqlite;
using SimonCLI.Domain.Models;
using SimonCLI.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

bool isProcessing = true;

Dictionary<string, string> commands = new()
{
    { "Add a user", "a" },
    { "List registered users", "l" },
    { "Quit", "q" },
};

using (var connection = new SqliteConnection("Data Source=database.db;"))
{

    // Open the connection:
    try
    {
        await connection.OpenAsync();
        Console.WriteLine("Connection worked!");

        var createUserTableQuery = @"
        CREATE TABLE IF NOT EXISTS Users(
            ID INTEGER PRIMARY KEY,
            FirstName TEXT NOT NULL,
            LastName TEXT NOT NULL)";

        await connection.ExecuteReaderAsync(createUserTableQuery);
    }
    catch (Exception e)
    {
        await connection.ExecuteReaderAsync("DROP TABLE IF EXISTS Users");
        Console.WriteLine(e);
    }
    var repository = new UsersRepository(connection);

    Console.WriteLine("Welcome 2 Simon CLI!");
    Thread.Sleep(1000);
    AddLogo();

    var users = await repository.GetAsync();

    AddSpace(2);
    if (users.Count == 0)
    {
        Console.WriteLine("Introduce yourself to be our first user.");
        var user = await CreateUserAsync(repository);
        Console.WriteLine($"Congratulations {user.Name}!");
    }

    AddSpace(1);
    Console.WriteLine("You can choose to do an action.");

    while (isProcessing)
    {
        DisplayActions(commands);

        switch ((Console.ReadLine() ?? "").ToLower())
        {
            case "l":
                Console.WriteLine("Here are the users already registered:");
                AddSpace(1);
                foreach (var registeredUsers in await repository.GetAsync())
                    Console.WriteLine($"- {registeredUsers.Name}");
                AddSpace(1);
                break;

            case "a":
                Console.WriteLine("Adding a user.");
                await CreateUserAsync(repository);

                AddSpace(1);

                users = await repository.GetAsync();

                Console.WriteLine($"User {users.Last().Name} added!");
                Console.WriteLine("Here are all the users:");
                AddSpace(1);
                foreach (var user in users) Console.WriteLine($"- {user.Name}");
                AddSpace(1);
                break;

            case "q":
                Console.WriteLine("Thanks for coming by!");
                isProcessing = false;
                break;

            default:
                AddSpace(1);
                Console.WriteLine("I do not know this command...");
                AddSpace(1);
                break;
        }
    }

    connection.Close();
    Console.WriteLine("Connection closed");
}

static Task<User> CreateUserAsync(UsersRepository repository)
{
    Console.Write("first name: ");
    var firstName = Console.ReadLine() ?? "";

    Console.Write("last name: ");
    var lastName = Console.ReadLine() ?? "";

    return repository.CreateAsync(new User(firstName, lastName));
}

static void DisplayActions(Dictionary<string, string> commands)
{
    Console.WriteLine("What do you want to do now?");
    foreach (var (action, command) in commands)
    {
        Console.WriteLine($"{action} [{command.ToUpper()}]");
    }
    Console.Write("Action: ");
}

static void AddLogo()
{
    Console.WriteLine(@"
      #############
     ##
    ##
     ##
      ############
                 ##
                  ##
                 ##
     #############          
    ");
}

static void AddSpace(int lines)
{
    for (int i = 0; i < lines; i++) Console.WriteLine();
}
