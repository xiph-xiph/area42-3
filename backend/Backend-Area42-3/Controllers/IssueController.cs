using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend_Area42_3.Services;
using Backend_Area42_3.Models;
using Backend_Area42_3.DTO.Output;

namespace Backend_Area42_3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssueController(IssueService issueService) : ControllerBase
{
    private readonly IssueService issueService = issueService;

    [HttpGet("get/all")]
    public Task<List<Issue>> GetAllIssues()
    {
        return issueService.GetAllIssues();
    }

    [HttpGet("get/unsolved")]
    public Task<List<Issue>> GetAllUnsolvedIssues()
    {
        return issueService.GetAllIssues("unsolved");
    }

    [HttpGet("get/solved")]
    public Task<List<Issue>> GetAllSolvedIssues()
    {
        return issueService.GetAllIssues("solved");
    }

    [HttpPost("create")]
    [Authorize]
    public Task<SuccessMessageDto> ReportIssue(Issue issue)
    {
        return issueService.ReportIssue(issue);
    }

    [HttpPost("resolve")]
    public Task<SuccessMessageDto> ResolveIssue(int issueId)
    {
        return issueService.ResolveIssue(issueId);
    }
}