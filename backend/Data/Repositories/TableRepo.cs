using Backend_Area42_3.Models;
using Backend_Area42_3.Enums;
using Npgsql;

namespace Backend_Area42_3.Repositories;

public class TableRepo(NpgsqlDataSource dataSource) : ITableRepo
{
    private readonly NpgsqlDataSource dataSource = dataSource;

    public async Task<List<Table>> GetAll()
    {
        var result = new List<Table>();
        using var connection = await dataSource.OpenConnectionAsync();
        string query = "SELECT id, restaurant::text, max_guests FROM tables";
        using var command = new NpgsqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(new Table
            {
                Id = reader.GetInt32(0),
                Restaurant = Enum.Parse<Restaurant>(reader.GetString(1), true),
                MaxGuests = reader.GetInt32(2)
            });
        }
        return result;
    }

    public async Task<Table?> GetById(int id)
    {
        using var connection = await dataSource.OpenConnectionAsync();
        string query = "SELECT id, restaurant::text, max_guests FROM tables WHERE id = @id";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Table
            {
                Id = reader.GetInt32(0),
                Restaurant = Enum.Parse<Restaurant>(reader.GetString(1), true),
                MaxGuests = reader.GetInt32(2)
            };
        }
        return null;
    }
}
