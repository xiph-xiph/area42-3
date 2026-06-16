
// ExampleRepo.cs
using Npgsql;
namespace Backend_Area42_3.Repositories;

public class ExampleRepo : IExampleRepo
{
    private readonly string _connectionString;

    public ExampleRepo(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Example> GetAll()
    {
        var result = new List<Example>();

        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        var cmd = new NpgsqlCommand("SELECT * FROM examples", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            result.Add(new Example
            {
                Id = reader.GetInt32(0)
            });
        }

        return result;
    }

    public Example? GetById(int id)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        var cmd = new NpgsqlCommand("SELECT * FROM examples WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Example
            {
                Id = reader.GetInt32(0)
            };
        }

        return null;
    }

    public void Add(Example example)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        var cmd = new NpgsqlCommand("INSERT INTO examples (id) VALUES (@id)", conn);
        cmd.Parameters.AddWithValue("@id", example.Id);

        cmd.ExecuteNonQuery();
    }

    public void Update(Example example)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        var cmd = new NpgsqlCommand("UPDATE examples SET id = @id WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("@id", example.Id);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        var cmd = new NpgsqlCommand("DELETE FROM examples WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
}