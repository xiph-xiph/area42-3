using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public interface IOrderItemRepository
{
    Task<int> CreateItem(int orderId, OrderItem orderItem);
    Task<bool> UpdateItem(OrderItem orderItem);
    Task<bool> DeleteItem(int itemId);
}