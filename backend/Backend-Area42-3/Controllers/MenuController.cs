using Backend_Area42_3.DTO.Output;
using Backend_Area42_3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend_Area42_3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController(MenuService _menuService) : ControllerBase
{
    private readonly MenuService menuService = _menuService;

    [HttpGet]
    [Authorize]
    public async Task<MenuDto> GetMenuItems()
    {
        return await menuService.GetMenuItems();
    }
}
