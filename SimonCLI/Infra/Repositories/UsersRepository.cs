using Dapper;
using Microsoft.Data.Sqlite;
using SimonCLI.Domain.Models;
using SimonCLI.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimonCLI.Infra.Repositories;
internal class UsersRepository : IUsersRepository
{
    private readonly SqliteConnection _connection;

    public UsersRepository(SqliteConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<User>> GetAsync()
    {
        var query = "SELECT * FROM USERS";
        return (await _connection.QueryAsync<User>(query)).ToList() ?? [];
    }

    public async Task<User> CreateAsync(User user)
    {
        SqliteCommand cmd = _connection.CreateCommand();

        // Set the command text (INSERT statement)
        cmd.CommandText = $"INSERT INTO Users (FirstName, LastName) VALUES (@firstName, @lastName)";

        // Add parameters
        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@lastName", user.LastName);

        await cmd.ExecuteNonQueryAsync();

        return user;
    }
}
