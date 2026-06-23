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
        command.Parameters.AddWithValue("@Name", issue.Name);
        command.Parameters.AddWithValue("@Description", issue.Description);
        command.Parameters.AddWithValue("@CreationDate", DateTime.Now);
        command.Parameters.AddWithValue("@SolvedDate", issue.SolvedDate ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Solved", issue.Solved);


        return (Issue?)await command.ExecuteScalarAsync();
    }

    public async Task<Issue?> UpdateIssue(Issue oldIssue, Issue newIssue)
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string query = 
         @"
            UPDATE issues
            SET
                user_id = @UserId,
                priority = @Priority::public.priority,
                category = @Category::public.issue_category,
                name = @Name,
                description = @Description,
                creation_date = @CreationDate,
                solved_date = @SolvedDate,
                solved = @Solved
            WHERE
                id = @Id;
        ";


        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", newIssue.UserId);
        command.Parameters.AddWithValue("@Priority", newIssue.Priority.ToString().ToLower());
        command.Parameters.AddWithValue("@Category", newIssue.Category.ToString().ToLower()); // voeg toe automatisch prioriteit
        command.Parameters.AddWithValue("@Name", newIssue.Name);
        command.Parameters.AddWithValue("@Description", newIssue.Description);
        command.Parameters.AddWithValue("@CreationDate", DateTime.Now);
        command.Parameters.AddWithValue("@SolvedDate", newIssue.SolvedDate ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@Solved", newIssue.Solved);
        command.Parameters.AddWithValue("@Id", oldIssue.Id);

        return (Issue?)await command.ExecuteScalarAsync();

    }
    public async Task<Issue?> GetIssueById(int issueId)
    {
        using var connection = await dataSource.OpenConnectionAsync();

        string query = @"SELECT * FROM issues WHERE id = @Id;";

        using var command = new NpgsqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync()) {

            var foundIssue = new Issue
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                Priority = Enum.Parse<PriorityEnum>(reader.GetString(reader.GetOrdinal("priority")).ToLower(), ignoreCase: true),
                Category = Enum.Parse<CategoryEnum>(reader.GetString(reader.GetOrdinal("category")).ToLower(), ignoreCase: true),
                Name = reader.GetString(reader.GetOrdinal("name")),
                Description = reader.GetString(reader.GetOrdinal("description")),
                CreationDate = reader.GetDateTime(reader.GetOrdinal("creation_date")),
                SolvedDate = reader.IsDBNull(reader.GetOrdinal("solved_date")) ? null : reader.GetDateTime(reader.GetOrdinal("solved_date")),
                Solved = reader.GetBoolean(reader.GetOrdinal("solved"))
            };
            return foundIssue;
        }
        return null;
    }

    public async Task<List<Issue>> GetAll(string filter = "")
    {
        using var connection = await dataSource.OpenConnectionAsync();
        string query = @"SELECT * FROM issues;";

        switch (filter)
        {
            case "solved":
                query = @"SELECT * FROM issues WHERE solved = true;";
                break;
            case "unsolved":
                query = @"SELECT * FROM issues WHERE solved = false;";
                break;
        }

        using var command = new NpgsqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        var issues = new List<Issue>();

        while (await reader.ReadAsync())
        {
            issues.Add(new Issue
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                Priority = Enum.Parse<PriorityEnum>(reader.GetString(reader.GetOrdinal("priority")).ToLower(), ignoreCase: true),
                Category = Enum.Parse<CategoryEnum>(reader.GetString(reader.GetOrdinal("category")).ToLower(), ignoreCase: true),
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