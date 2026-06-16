using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public class UserRepository : IUserRepository
{
    public Task CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(int id)
    {
        throw new NotImplementedException();
    }
}
