using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public interface IOrderItemRepository
{
    Task<bool> CreateItem(int orderId, int menuItemId, int quantity);
    Task<bool> UpdateItem(int orderId, int itemId, int newQuantity);
    Task<bool> DeleteItem(int orderId, int itemId);
}