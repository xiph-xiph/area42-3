using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;
using Backend_Area42_3.Repositories;
using Backend_Area42_3.Models;

namespace Backend_Area42_3.Services;

public class OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
{
    private readonly IOrderRepository orderRepository = orderRepository;
    private readonly IOrderItemRepository orderItemRepository = orderItemRepository;

    public async Task<CartDto> GetCart(int userId)
    {
        var cart = await GetOrCreateCart(userId);
        return new CartDto
        {
            Success = cart != null,
            Message = cart != null ? "Cart retrieved successfully" : "No cart found",
            Cart = cart
        };
    }

    private async Task<Cart?> GetOrCreateCart(int userId)
    {
        var cart = await orderRepository.GetCart(userId);
        if (cart == null)
        {
            var created = await CreateCart(userId);
            if (!created)
            {
                return null;
            }
            cart = await orderRepository.GetCart(userId);
        }
        return cart;
    }

    private async Task<bool> CreateCart(int userId)
    {
        var orderId = await orderRepository.CreateOrder(
            new Order
            {
                Id = null,
                UserId = userId,
                Status = Enums.OrderStatus.Cart,
                TotalPrice = 0,
                PickupTime = DateTime.Now,
                Items = []
            });
        return orderId > 0;
    }

    public async Task<SuccessMessageDto> AddItemToCart(AddItemDto addItemDto, int userId)
    {
        var cart = await GetOrCreateCart(userId);
        if (cart == null || cart.Id == null)
        {
            return new SuccessMessageDto
            {
                Success = false,
                Message = "Failed to find or create cart"
            };
        }

        foreach (var item in cart.Items)
        {
            if (item.Id == addItemDto.MenuItemId)
            {
                // Als het item al in de cart zit, update dan de hoeveelheid
                int newQuantity = item.Quantity + addItemDto.Quantity;
                await orderItemRepository.UpdateItem((int)cart.Id, item.Id, newQuantity);
                return new SuccessMessageDto
                {
                    Success = true,
                    Message = "Item quantity updated in cart successfully"
                };
            }
        }

        await orderItemRepository.CreateItem((int)cart.Id, addItemDto.MenuItemId, addItemDto.Quantity);
        return new SuccessMessageDto
        {
            Success = true,
            Message = "Item added to cart successfully"
        };
    }

    public async Task<SuccessMessageDto> RemoveItemFromCart(RemoveItemDto removeItemDto, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<SuccessMessageDto> CheckoutCart()
    {
        throw new NotImplementedException();
    }

    public async Task<OrderListDto> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public async Task<OrderListDto> GetUserOrders()
    {
        throw new NotImplementedException();
    }
}