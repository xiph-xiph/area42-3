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
                     o.name,
                     o.phone,
                     o.remarks,
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
            var itemsJson = reader.GetString(8);
            List<OrderItem> items = JsonSerializer.Deserialize<List<OrderItem>>(itemsJson) ?? [];
            return new Cart
            {
                Id = reader.GetInt32(0),
                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                Remarks = reader.IsDBNull(3) ? null : reader.GetString(3),
                UserId = reader.GetInt32(4),
                Status = Enum.Parse<OrderStatus>(reader.GetString(5), ignoreCase: true),
                TotalPrice = reader.GetDecimal(6),
                PickupTime = reader.IsDBNull(7) ? null : reader.GetDateTime(7),
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
                     o.name,
                     o.phone,
                     o.remarks,
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
              GROUP BY o.id
              LIMIT 1;
            ";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@OrderId", orderId);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var itemsJson = reader.GetString(8);
            List<OrderItem> items = JsonSerializer.Deserialize<List<OrderItem>>(itemsJson) ?? [];
            return new Order
            {
                Id = reader.GetInt32(0),
                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                Remarks = reader.IsDBNull(3) ? null : reader.GetString(3),
                UserId = reader.GetInt32(4),
                Status = Enum.Parse<OrderStatus>(reader.GetString(5), ignoreCase: true),
                TotalPrice = reader.GetDecimal(6),
                PickupTime = reader.IsDBNull(7) ? null : reader.GetDateTime(7),
                Items = items
            };
        }

        return null;
    }

    public async Task<List<Order>> GetOrdersByUserId(int userId)
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string query =
            @"
              SELECT o.id,
                     o.name,
                     o.phone,
                     o.remarks,
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
                AND o.status != 'cart'
              GROUP BY o.id;
            ";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", userId);

        using var reader = await command.ExecuteReaderAsync();
        List<Order> orders = [];
        while (await reader.ReadAsync())
        {
            var itemsJson = reader.GetString(8);
            List<OrderItem> items = JsonSerializer.Deserialize<List<OrderItem>>(itemsJson) ?? [];
            orders.Add(new Order
            {
                Id = reader.GetInt32(0),
                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                Remarks = reader.IsDBNull(3) ? null : reader.GetString(3),
                UserId = reader.GetInt32(4),
                Status = Enum.Parse<OrderStatus>(reader.GetString(5), ignoreCase: true),
                TotalPrice = reader.GetDecimal(6),
                PickupTime = reader.IsDBNull(7) ? null : reader.GetDateTime(7),
                Items = items
            });
        }

        return orders;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string query =
            @"
              SELECT o.id,
                     o.name,
                     o.phone,
                     o.remarks,
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
              WHERE o.status != 'cart'
              GROUP BY o.id;
            ";
        using var command = new NpgsqlCommand(query, connection);

        using var reader = await command.ExecuteReaderAsync();
        List<Order> orders = [];
        while (await reader.ReadAsync())
        {
            var itemsJson = reader.GetString(8);
            List<OrderItem> items = JsonSerializer.Deserialize<List<OrderItem>>(itemsJson) ?? [];
            orders.Add(new Order
            {
                Id = reader.GetInt32(0),
                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                Remarks = reader.IsDBNull(3) ? null : reader.GetString(3),
                UserId = reader.GetInt32(4),
                Status = Enum.Parse<OrderStatus>(reader.GetString(5), ignoreCase: true),
                TotalPrice = reader.GetDecimal(6),
                PickupTime = reader.IsDBNull(7) ? null : reader.GetDateTime(7),
                Items = items
            });
        }

        return orders;
    }

    public async Task<bool> UpdateOrder(Order order)
    {
        using var connection = await dataSource.OpenConnectionAsync();
        string query =
            @"
                UPDATE takeaway_orders
                SET
                    user_id = @UserId,
                    name = @Name,
                    phone = @Phone,
                    remarks = @Remarks,
                    status = @Status::order_status,
                    total_price = @TotalPrice,
                    pickup_time = @PickupTime
                WHERE id = @Id
            ";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", order.UserId);
        command.Parameters.AddWithValue("@Name", order.Name ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Phone", order.Phone ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Remarks", order.Remarks ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Status", order.Status.ToString().ToLowerInvariant());
        command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
        command.Parameters.AddWithValue("@PickupTime", order.PickupTime ?? (object)DBNull.Value);
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
                    total_price
                )
                VALUES
                (
                    @UserId,
                    @Status::order_status,
                    @TotalPrice
                )
                RETURNING id;
            ";

        using var command = new NpgsqlCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@UserId", order.UserId);
        command.Parameters.AddWithValue("@Status", order.Status.ToString().ToLowerInvariant());
        command.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);

        return ((int?)await command.ExecuteScalarAsync()) ?? 0;
    }
}
