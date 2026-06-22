namespace Backend_Area42_3.Repositories;

using Backend_Area42_3.Models;
using Backend_Area42_3.Enums;
using Npgsql;

public class IssueRepository(NpgsqlDataSource dataSource) : IIssueRepository
{
    private readonly NpgsqlDataSource dataSource = dataSource;

    public async Task<Issue?> CreateIssue(Issue issue)
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string query =
            @"
                INSERT INTO issues
                (
                    user_id,
                    priority,
                    category,
                    name,
                    description,
                    creation_date,
                    solved_date,
                    solved
                )
                VALUES
                (
                    @UserId,
                    @Priority,
                    @Category,
                    @Name,
                    @Description,
                    @CreationDate,
                    @SolvedDate,
                    @Solved
                )
                RETURNING id;
            ";

        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", issue.UserId);
        command.Parameters.AddWithValue("@Priority", issue.Priority.ToString());
        command.Parameters.AddWithValue("@Category", issue.Category.ToString()); // voeg toe automatisch prioriteit
        command.Parameters.AddWithValue("@Name", issue.Category.ToString());
        command.Parameters.AddWithValue("@Description", issue.Category.ToString());
        command.Parameters.AddWithValue("@CreationDate", DateTime.Now);
        command.Parameters.AddWithValue("@SolvedDate", issue.SolvedDate ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Solved", issue.Solved);


        return (Issue?)await command.ExecuteScalarAsync();
    }

    //public async Task<Issue?> UpdateIssue(Issue oldIssue, Issue newIssue)
    //{
    //}
    //public async Task<Issue?> GetIssueById(Issue test)
    //{
    //}
    public async Task<List<Issue>> GetAll()
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string query = @"SELECT * FROM issues;";

        using var command = new NpgsqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        var issues = new List<Issue>();

        while (await reader.ReadAsync())
        {
            issues.Add(new Issue
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                Priority = Enum.Parse<PriorityEnum>(reader.GetString(reader.GetOrdinal("priority")), ignoreCase: true),
                Category = Enum.Parse<CategoryEnum>(reader.GetString(reader.GetOrdinal("category")), ignoreCase: true),
                Name = reader.GetString(reader.GetOrdinal("name")),
                Description = reader.GetString(reader.GetOrdinal("description")),
                CreationDate = reader.GetDateTime(reader.GetOrdinal("creation_date")),
                SolvedDate = reader.IsDBNull(reader.GetOrdinal("solved_date")) ? null : reader.GetDateTime(reader.GetOrdinal("solved_date")),
                Solved = reader.GetBoolean(reader.GetOrdinal("solved"))
            });
        }

        return issues;
    }
}