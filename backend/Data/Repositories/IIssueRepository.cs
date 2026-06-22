using Backend_Area42_3.Models;

namespace Backend_Area42_3.Repositories;

public interface IIssueRepository
{
    Task<Issue?> CreateIssue(Issue issue);
    //Task<Issue?> UpdateIssue(Issue oldIssue, Issue newIssue);
    //Task<Issue?> GetIssueById(Guid issueId);
    Task<List<Issue?>> GetAll();
}