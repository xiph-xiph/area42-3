using Area52Horeca.Interfaces;
using Area52Horeca.Models;
using Npgsql;

namespace Area52Horeca.Repositories
{
    public class TijdslotRepository : ITijdslotRepository
    {
        private readonly string _connectionString;

        public TijdslotRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public List<Tijdslot> GetAll()
        {
            throw new NotImplementedException();
        }

        public Tijdslot GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Tijdslot> GetAvailable(DateTime datum)
        {
            throw new NotImplementedException();
        }

        public void Add(Tijdslot tijdslot)
        {
            throw new NotImplementedException();
        }
    }
}