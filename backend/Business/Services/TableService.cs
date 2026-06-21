using Backend_Area42_3.Repositories;
using Backend_Area42_3.Models;

namespace Backend_Area42_3.Services;

public class TableService(ITableRepo tableRepo)
{
    private readonly ITableRepo _tableRepo = tableRepo;

    public async Task<List<Table>> GetAll()
    {
        return await _tableRepo.GetAll();
    }

    public async Task<Table?> GetById(int id)
    {
        return await _tableRepo.GetById(id);
    }
}
