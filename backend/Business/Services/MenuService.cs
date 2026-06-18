namespace Backend_Area42_3.Services;

using Backend_Area42_3.DTO.Output;
using Backend_Area42_3.Models;
using Backend_Area42_3.Repositories;

public class MenuService(IMenuRepository _menuRepository)
{
    private readonly IMenuRepository menuRepository = _menuRepository;

    public async Task<MenuDto> GetMenuItems()
    {
        List<MenuItem> menuItems = await menuRepository.GetMenuItems();
        return new MenuDto
        {
            Success = true,
            Message = "Menu retrieved successfully",
            Menu = menuItems
        };
    }
}