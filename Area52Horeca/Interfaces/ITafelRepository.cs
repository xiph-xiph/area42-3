using Area52Horeca.Models;

namespace Area52Horeca.Interfaces
{
    public interface ITafelRepository
    {
        List<Tafel> GetAll();
        Tafel GetById(int id);
        List<Tafel> GetAvailable(DateTime datum);
        void Delete(int id);
    }
}