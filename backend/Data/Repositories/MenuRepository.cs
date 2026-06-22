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
            "SELECT id, title, price, image_url, category, is_popular FROM menu_items";
        using var command = new NpgsqlCommand(query, connection);

        using var reader = await command.ExecuteReaderAsync();
        var menuItems = new List<MenuItem>();
        while (await reader.ReadAsync())
        {
            menuItems.Add(new MenuItem
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Price = reader.GetDecimal(2),
                ImageUrl = reader.GetString(3),
                Category = reader.GetString(4),
                IsPopular = reader.GetBoolean(5)
            });
        }
        return menuItems;
    }
}