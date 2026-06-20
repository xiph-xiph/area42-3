using Npgsql;

namespace Backend_Area42_3.Repositories;

public class OrderItemRepository(NpgsqlDataSource dataSource) : IOrderItemRepository
{
	private readonly NpgsqlDataSource dataSource = dataSource;

	public async Task<bool> CreateItem(int orderId, int menuItemId, int quantity)
	{
		using var connection = await dataSource.OpenConnectionAsync();

		string query =
			@"
                INSERT INTO order_items
                (
                    order_id,
					item_id,
					quantity
                )
                VALUES
                (
                    @OrderId,
                    @ItemId,
                    @Quantity
                )
            ";

		using var command = new NpgsqlCommand(query, connection);
		command.Parameters.AddWithValue("@OrderId", orderId);
		command.Parameters.AddWithValue("@ItemId", menuItemId);
		command.Parameters.AddWithValue("@Quantity", quantity);

		await command.ExecuteNonQueryAsync();
		return true;
	}

	public async Task<bool> UpdateItem(int orderId, int itemId, int newQuantity)
	{
		using var connection = await dataSource.OpenConnectionAsync();

		string query =
			@"
                UPDATE order_items
                SET quantity = @NewQuantity
                WHERE order_id = @OrderId AND item_id = @ItemId
            ";

		using var command = new NpgsqlCommand(query, connection);
		command.Parameters.AddWithValue("@NewQuantity", newQuantity);
		command.Parameters.AddWithValue("@OrderId", orderId);
		command.Parameters.AddWithValue("@ItemId", itemId);

		await command.ExecuteNonQueryAsync();
		return true;
	}

	public Task<bool> DeleteItem(int orderId, int itemId)
	{
		throw new NotImplementedException();
	}
}
