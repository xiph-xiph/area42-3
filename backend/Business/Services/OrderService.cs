using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;

namespace Backend_Area42_3.Services;

public class OrderService
{
    public async Task<CartDto> GetCart()
    {
        throw new NotImplementedException();
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