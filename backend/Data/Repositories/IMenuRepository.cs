using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public interface IMenuRepository
{
    Task<List<MenuItem>> GetMenuItems();
}
