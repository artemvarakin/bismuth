using BismuthAPI.Abstractions;
using BismuthAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BismuthAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class IssueController : ControllerBase {
    private readonly IIssueRepository _issueRepository;
    private readonly IProjectRepository _projectRepository;

    public IssueController(IIssueRepository issueRepository, IProjectRepository projectRepository)
    {
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Issue>> GetIssueAsync(int id, CancellationToken token)
    {
        var issue = await _issueRepository.GetIssueAsync(id, token);
        if (issue is null) {
            return NotFound("Issue not found.");
        }

        return Ok(issue);
    }

    [HttpGet("project/{id}")]
    public async Task<ActionResult<IEnumerable<Issue>>> GetProjectIssues(int id, CancellationToken token)
    {
        var project = await _projectRepository.GetProjectAsync(id, token);
        if (project is null) {
            return NotFound("Project does not exist.");
        }

        var issues = await _issueRepository.GetProjectIssues(id, token);
        if (!issues.Any()) {
            return NotFound("Issues not found.");
        }

        return Ok(issues);
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<Issue>>> AddIssueAsync(Issue issue, CancellationToken token)
    {
        var project = await _projectRepository.GetProjectAsync(issue.ProjectId, token);
        if (project is null) {
            return NotFound("Project does not exist.");
        }

        var issues = await _issueRepository.AddIssueAsync(issue, token);
        return Ok(issues);
    }

    [HttpPut]
    public async Task<ActionResult<Issue>> UpdateIssueAsync(Issue issue, CancellationToken token)
    {
        var targetIssue = await _issueRepository.GetIssueAsync(issue.Id, token);
        if (targetIssue is null) {
            return NotFound("Issue not found.");
        }

        var project = await _projectRepository.GetProjectAsync(issue.ProjectId, token);
        if (project is null) {
            return NotFound("Project does not exist.");
        }

        var updatedIssue = await _issueRepository.UpdateIssueAsync(issue, token);
        return Ok(updatedIssue);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteIssueAsync(int id, CancellationToken token)
    {
        var targetIssue = await _issueRepository.GetIssueAsync(id, token);
        if (targetIssue is null) {
            return NotFound("Issue not found.");
        }

        var availableProjectIssues = await _issueRepository.DeleteIssueAsync(targetIssue, token);
        return Ok(availableProjectIssues);
    }
}