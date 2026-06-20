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
                Name = null,
                Phone = null,
                Remarks = null,
                UserId = userId,
                Status = Enums.OrderStatus.Cart,
                TotalPrice = 0,
                PickupTime = null,
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
                await UpdateOrderTotal((int)cart.Id);
                return new SuccessMessageDto
                {
                    Success = true,
                    Message = "Item quantity updated in cart successfully"
                };
            }
        }

        await orderItemRepository.CreateItem((int)cart.Id, addItemDto.MenuItemId, addItemDto.Quantity);
        await UpdateOrderTotal((int)cart.Id);
        return new SuccessMessageDto
        {
            Success = true,
            Message = "Item added to cart successfully"
        };
    }

    public async Task<SuccessMessageDto> RemoveItemFromCart(RemoveItemDto removeItemDto, int userId)
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
            if (item.Id == removeItemDto.OrderItemId)
            {
                int newQuantity = item.Quantity - (removeItemDto.Quantity ?? item.Quantity);
                if (newQuantity <= 0)
                {
                    await orderItemRepository.DeleteItem((int)cart.Id, item.Id);
                    await UpdateOrderTotal((int)cart.Id);
                    return new SuccessMessageDto
                    {
                        Success = true,
                        Message = "Item removed from cart successfully"
                    };
                }
                else
                {
                    await orderItemRepository.UpdateItem((int)cart.Id, item.Id, newQuantity);
                    await UpdateOrderTotal((int)cart.Id);
                    return new SuccessMessageDto
                    {
                        Success = true,
                        Message = "Item quantity updated in cart successfully"
                    };
                }
            }
        }

        return new SuccessMessageDto
        {
            Success = false,
            Message = "Item not found in cart"
        };
    }

    private async Task<bool> UpdateOrderTotal(int orderId)
    {
        var order = await orderRepository.GetOrder(orderId);
        if (order == null)
        {
            return false;
        }

        decimal totalPrice = 0;
        foreach (var item in order.Items)
        {
            totalPrice += item.Price * item.Quantity;
        }
        order.TotalPrice = totalPrice;

        await orderRepository.UpdateOrder(order);
        return true;
    }

    public async Task<SuccessMessageDto> CheckoutCart(CheckoutDto checkoutDto, int userId)
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

        if (cart.Items.Count == 0)
        {
            return new SuccessMessageDto
            {
                Success = false,
                Message = "Cart is empty"
            };
        }

        await orderRepository.UpdateOrder(new Order
        {
            Id = cart.Id,
            Name = checkoutDto.Name,
            Phone = checkoutDto.Phone,
            Remarks = checkoutDto.Remarks,
            UserId = userId,
            Status = Enums.OrderStatus.Scheduled,
            TotalPrice = cart.TotalPrice,
            PickupTime = checkoutDto.PickupTime,
            Items = cart.Items
        });
        return new SuccessMessageDto
        {
            Success = true,
            Message = "Checkout successful"
        };
    }

    public async Task<OrderListDto> GetAllOrders()
    {
        var orders = await orderRepository.GetAllOrders();
        return new OrderListDto
        {
            Success = true,
            Message = "Orders retrieved successfully",
            Orders = orders
        };
    }

    public async Task<OrderListDto> GetUserOrders(int userId)
    {
        var orders = await orderRepository.GetOrdersByUserId(userId);
        return new OrderListDto
        {
            Success = true,
            Message = "Orders retrieved successfully",
            Orders = orders
        };
    }
}