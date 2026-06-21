namespace Backend_Area42_3.Repositories;

using Backend_Area42_3.Models;
using Npgsql;

public class MenuRepository(NpgsqlDataSource dataSource) : IMenuRepository
{
    private readonly NpgsqlDataSource dataSource = dataSource;

    public async Task<List<MenuItem>> GetMenuItems()
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string query =
            "SELECT id, price, name, description FROM menu_items";
        using var command = new NpgsqlCommand(query, connection);

        using var reader = await command.ExecuteReaderAsync();
        var menuItems = new List<MenuItem>();
        while (await reader.ReadAsync())
        {
            menuItems.Add(new MenuItem
            {
                Id = reader.GetInt32(0),
                Price = reader.GetDecimal(1),
                Name = reader.GetString(2),
                Description = reader.GetString(3)
            });
        }
        return menuItems;
    }
}