using Backend_Area42_3.Models;
using Npgsql;

namespace Backend_Area42_3.Repositories;

public class UserRepository(NpgsqlDataSource dataSource) : IUserRepository
{
    private readonly NpgsqlDataSource _dataSource = dataSource;

    public async Task CreateUser(User user)
    {
        using var connection = await _dataSource.OpenConnectionAsync();

        string insertQuery =
            @"
                INSERT INTO users (name, email, phone, password_hash, role)
                VALUES (@Name, @Email, @Phone, @PasswordHash, @Role::user_role);
            ";

        using var command = new NpgsqlCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Name", user.Name);
        command.Parameters.AddWithValue("@Email", user.Email);
        command.Parameters.AddWithValue("@Phone", (object?)user.Phone ?? DBNull.Value);
        command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
        command.Parameters.AddWithValue("@Role", user.Role.ToString().ToLowerInvariant());

        await command.ExecuteNonQueryAsync();
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
