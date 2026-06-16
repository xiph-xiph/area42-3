using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task<User> GetUserById(int id);
    Task<User> GetUserByEmail(string email);
}
