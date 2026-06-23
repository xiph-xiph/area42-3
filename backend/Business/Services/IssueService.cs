using Backend_Area42_3.DTO.Input;
using Backend_Area42_3.DTO.Output;
using Backend_Area42_3.Repositories;
using Backend_Area42_3.Models;
using System.Diagnostics.Eventing.Reader;

namespace Backend_Area42_3.Services;

public class IssueService(IIssueRepository issueRepository)
{
    private readonly IIssueRepository issueRepository = issueRepository;

    public async Task<SuccessMessageDto> ReportIssue(Issue issue)
    {
        var newIssue = await issueRepository.CreateIssue(issue);

        if (newIssue != null) {
            return new SuccessMessageDto
            {
                Success = true,
                Message = "New issue created"
            };
        } else {
            return new SuccessMessageDto
            {
                Success = false,
                Message = "Failed to create issue"
            };
        }
    }

    public async Task<List<Issue>> GetAllIssues(string filter = "")
    {
        return await issueRepository.GetAll(filter);
    }
    public async Task<SuccessMessageDto> ResolveIssue(int issueId)
    {
        var IssueToChange = await issueRepository.GetIssueById(issueId);

        if (IssueToChange != null)
        {
            var response = await issueRepository.UpdateIssue(IssueToChange, new Issue
            {
                Id = IssueToChange.Id,
                UserId = IssueToChange.UserId,
                Priority = IssueToChange.Priority,
                Category = IssueToChange.Category,
                Name = IssueToChange.Name,
                Description = IssueToChange.Description,
                CreationDate = IssueToChange.CreationDate,
                SolvedDate = DateTime.Now,
                Solved = true
            });
            if (response != null)
            {
                return new SuccessMessageDto
                {
                    Success = true,
                    Message = "Issue resolved"
                };
            }

            return new SuccessMessageDto
            {
                Success = false,
                Message = "Unable to resolve issue"
            };

        }

        return new SuccessMessageDto
        {
            Success = false,
            Message = "Failed to select issue"
        };
    }
}