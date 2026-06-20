using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public class OrderRepository : IOrderRepository
{
	public Task<Cart> GetCart(int userId)
	{
		throw new NotImplementedException();
	}

	public Task<Order> GetOrder(int orderId)
	{
		throw new NotImplementedException();
	}

	public Task<List<Order>> GetOrdersByUserId(int userId)
	{
		throw new NotImplementedException();
	}

	public Task<List<Order>> GetAllOrders()
	{
		throw new NotImplementedException();
	}

	public Task<bool> UpdateOrder(Order order)
	{
		throw new NotImplementedException();
	}

	public Task<int> CreateOrder(Order order)
	{
		throw new NotImplementedException();
	}
}
