using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
	public Task<int> CreateItem(int orderId, OrderItem orderItem)
	{
		throw new NotImplementedException();
	}

	public Task<bool> UpdateItem(int orderId, OrderItem orderItem)
	{
		throw new NotImplementedException();
	}

	public Task<bool> DeleteItem(int orderId, int itemId)
	{
		throw new NotImplementedException();
	}
}
