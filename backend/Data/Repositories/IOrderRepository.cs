using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public interface IOrderRepository
{
    Task<Cart?> GetCart(int userId);
    Task<Order?> GetOrder(int orderId);
    Task<List<Order>> GetOrdersByUserId(int userId);
    Task<List<Order>> GetAllOrders();
    Task<bool> UpdateOrder(Order order);
    Task<int> CreateOrder(Order order);
}