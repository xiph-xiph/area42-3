using Backend_Area42_3.Models;
using Backend_Area42_3.Enums;
using Npgsql;
using System.Text.Json;

namespace Backend_Area42_3.Repositories;

public class OrderRepository(NpgsqlDataSource dataSource) : IOrderRepository
{
    private readonly NpgsqlDataSource dataSource = dataSource;

    public async Task<Cart?> GetCart(int userId)
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string query =
            @"
              SELECT o.id,
                     o.user_id,
                     o.status,
                     o.total_price,
                     o.pickup_time,
                     COALESCE(
                         JSON_AGG(
                             JSON_BUILD_OBJECT(
                                 'Id', mi.id,
                                 'Name', mi.name,
                                 'Price', mi.price,
                                 'Description', mi.description,
                                 'Quantity', oi.quantity
                             )
                         ) FILTER (WHERE mi.id IS NOT NULL),
                         '[]'
                     ) AS items
              FROM takeaway_orders o
              LEFT JOIN order_items oi
                  ON o.id = oi.order_id
              LEFT JOIN menu_items mi
                  ON oi.item_id = mi.id
              WHERE o.user_id = @UserId
              AND o.status = 'cart'
              GROUP BY o.id;
            ";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", userId);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var itemsJson = reader.GetString(5);
            List<OrderItem> items = JsonSerializer.Deserialize<List<OrderItem>>(itemsJson) ?? [];
            return new Cart
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Status = Enum.Parse<OrderStatus>(reader.GetString(2), ignoreCase: true),
                TotalPrice = reader.GetDecimal(3),
                PickupTime = reader.GetDateTime(4),
                Items = items
            };
        }

        return null;
    }

    public async Task<Order?> GetOrder(int orderId)
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string query =
            @"
              SELECT o.id,
                     o.user_id,
                     o.status,
                     o.total_price,
                     o.pickup_time,
                     COALESCE(
                         JSON_AGG(
                             JSON_BUILD_OBJECT(
                                 'Id', mi.id,
                                 'Name', mi.name,
                                 'Price', mi.price,
                                 'Description', mi.description,
                                 'Quantity', oi.quantity
                             )
                         ) FILTER (WHERE mi.id IS NOT NULL),
                         '[]'
                     ) AS items
              FROM takeaway_orders o
              LEFT JOIN order_items oi
                  ON o.id = oi.order_id
              LEFT JOIN menu_items mi
                  ON oi.item_id = mi.id
              WHERE o.id = @OrderId
              GROUP BY o.id;
            ";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@OrderId", orderId);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var itemsJson = reader.GetString(5);
            List<OrderItem> items = JsonSerializer.Deserialize<List<OrderItem>>(itemsJson) ?? [];
            return new Cart
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Status = Enum.Parse<OrderStatus>(reader.GetString(2), ignoreCase: true),
                TotalPrice = reader.GetDecimal(3),
                PickupTime = reader.GetDateTime(4),
                Items = items
            };
        }

        return null;
    }

    public Task<List<Order>> GetOrdersByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateOrder(Order order)
    {
        using var connection = await dataSource.OpenConnectionAsync();
        string query =
            @"
                UPDATE takeaway_orders
                SET
                    user_id = @UserId,
                    status = @Status::order_status,
                    total_price = @TotalPrice,
                    pickup_time = @PickupTime
                WHERE id = @Id
            ";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", order.UserId);
        command.Parameters.AddWithValue("@Status", order.Status.ToString().ToLowerInvariant());
        command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
        command.Parameters.AddWithValue("@PickupTime", order.PickupTime);
        command.Parameters.AddWithValue("@Id", order.Id!);

        int rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    public async Task<int> CreateOrder(Order order)
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string insertQuery =
            @"
                INSERT INTO takeaway_orders
                (
                    user_id,
                    status,
                    total_price,
                    pickup_time
                )
                VALUES
                (
                    @UserId,
                    @Status::order_status,
                    @TotalPrice,
                    @PickupTime
                )
                RETURNING id;
            ";

        using var command = new NpgsqlCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@UserId", order.UserId);
        command.Parameters.AddWithValue("@Status", order.Status.ToString().ToLowerInvariant());
        command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
        command.Parameters.AddWithValue("@PickupTime", order.PickupTime);

        return ((int?)await command.ExecuteScalarAsync()) ?? 0;
    }
}
