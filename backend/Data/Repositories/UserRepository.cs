using Backend_Area42_3.Enums;
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

    public async Task<User?> GetUserByEmail(string email)
    {
        using var connection = await _dataSource.OpenConnectionAsync();

        string query =
            "SELECT id, name, email, phone, password_hash, role::text FROM users WHERE email = @Email";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@Email", email);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Phone = reader.GetString(3),
                PasswordHash = reader.GetString(4),
                Role = Enum.Parse<UserRole>(reader.GetString(5), ignoreCase: true),
            };
        }

        return null;
    }

    public async Task<User?> GetUserById(int id)
    {
        using var connection = await _dataSource.OpenConnectionAsync();

        string query =
            "SELECT id, name, email, phone, password_hash, role::text FROM users WHERE id = @Id";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Phone = reader.GetString(3),
                PasswordHash = reader.GetString(4),
                Role = Enum.Parse<UserRole>(reader.GetString(5), ignoreCase: true),
            };
        }

        return null;
    }
}
