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
        var cart = await orderRepository.GetCart(userId);
        if (cart == null)
        {
            var created = await CreateCart(userId);
            if (!created)
            {
                return new CartDto
                {
                    Success = false,
                    Message = "Failed to create a new cart",
                    Cart = null
                };
            }
            cart = await orderRepository.GetCart(userId);
        }
        return new CartDto
        {
            Success = cart != null,
            Message = cart != null ? "Cart retrieved successfully" : "No cart found",
            Cart = cart
        };
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

    public async Task<SuccessMessageDto> AddItemToCart(AddItemDto addItemDto)
    {
        throw new NotImplementedException();
    }

    public async Task<SuccessMessageDto> RemoveItemFromCart(RemoveItemDto removeItemDto)
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