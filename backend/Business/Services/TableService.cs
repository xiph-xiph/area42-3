
using Backend_Area42_3.Repositories;
using Backend_Area42_3.Models;

namespace Backend_Area42_3.Services;

public class TableService
{
    private readonly ITableRepo _tableRepo;

    public TableService(ITableRepo tableRepo)
    {
        _tableRepo = tableRepo;
    }

    public List<Table> GetAll()
    {
        throw new NotImplementedException();
    }

    public Table? GetById(int id)
    {
        throw new NotImplementedException();
    }
}