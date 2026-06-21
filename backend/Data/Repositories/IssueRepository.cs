namespace Backend_Area42_3.Repositories;

using Backend_Area42_3.Models;
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
        command.Parameters.AddWithValue("@Category", issue.Catergory.ToString());
        command.Parameters.AddWithValue("@Name", issue.Catergory.ToString());
        command.Parameters.AddWithValue("@Description", issue.Catergory.ToString());
        command.Parameters.AddWithValue("@CreationDate", issue.CreationDate);
        command.Parameters.AddWithValue("@SolvedDate", issue.SolvedDate);
        command.Parameters.AddWithValue("@Solved", issue.Solved);


        return (Issue?)await command.ExecuteScalarAsync();
    }

    //public async Task<Issue?> UpdateIssue(Issue oldIssue, Issue newIssue)
    //{
    //}
    //public async Task<Issue?> GetIssueById(Issue test)
    //{
    //}
    //public async Task<List<Issue?>> GetAll()
    //{
    //}
}