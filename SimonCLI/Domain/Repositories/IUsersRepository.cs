using SimonCLI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimonCLI.Domain.Repositories;

public interface IUsersRepository
{
    Task<List<User>> GetAsync();
    Task<User> CreateAsync(User user);
}
