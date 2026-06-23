using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public interface IIssueRepository
{
    Task<Issue?> CreateIssue(Issue issue);
    Task<Issue?> UpdateIssue(Issue issueId, Issue newIssue);
    Task<Issue?> GetIssueById(int issueId);
    Task<List<Issue>> GetAll(string filter = "");
}