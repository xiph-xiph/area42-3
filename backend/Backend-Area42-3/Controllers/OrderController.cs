using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend_Area42_3.Services;
using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;

namespace Backend_Area42_3.Controllers;

[ApiController]
[Route("api/[controller]")]
class OrderController(OrderService orderService) : ControllerBase
{
    private readonly OrderService orderService = orderService;

    [HttpGet("cart")]
    [Authorize]
    public async Task<CartDto> GetCart()
    {
        return await orderService.GetCart();
    }

    [HttpPost("cart/add")]
    [Authorize]
    public async Task<SuccessMessageDto> AddItemToCart(AddItemDto addItemDto)
    {
        return await orderService.AddItemToCart(addItemDto);
    }

    [HttpPost("cart/remove")]
    [Authorize]
    public async Task<SuccessMessageDto> RemoveItemFromCart(RemoveItemDto removeItemDto)
    {
        return await orderService.RemoveItemFromCart(removeItemDto);
    }

    [HttpPost("cart/checkout")]
    [Authorize]
    public async Task<SuccessMessageDto> CheckoutCart()
    {
        return await orderService.CheckoutCart();
    }

    [HttpGet]
    [Authorize]
    public async Task<OrderListDto> GetOrders()
    {

        if (User.IsInRole("Admin"))
        {
            return await orderService.GetAllOrders();
        }
        else
        {
            return await orderService.GetUserOrders();
        }
    }
}