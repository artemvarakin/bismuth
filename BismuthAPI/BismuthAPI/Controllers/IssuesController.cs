using BismuthAPI.Abstractions.Services;
using BismuthAPI.Contracts.Issue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BismuthAPI.Controllers;

[Authorize]
public sealed class IssuesController : ApiController
{
    private readonly IIssueManagerService _issueManager;

    public IssuesController(IIssueManagerService issueManager)
    {
        _issueManager = issueManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssueAsync(CreateIssueRequest request, CancellationToken token)
    {
        var issue = await _issueManager.CreateIssueAsync(request, token);
        if (issue is null)
        {
            return NotFound("Specified project does not exist.");
        }

        return CreatedAtAction(nameof(GetIssueAsync), new { id = issue.Id }, issue);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetIssueAsync(Guid id, CancellationToken token)
    {
        var issue = await _issueManager.GetIssueAsync(id, token);
        if (issue is null)
        {
            return IssueNotFound();
        }

        return Ok(issue);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateIssueAsync(UpdateIssueRequest request, CancellationToken token)
    {
        var issue = await _issueManager.UpdateIssueAsync(request, token);
        if (issue is null)
        {
            return IssueNotFound();
        }

        return Ok(issue);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteIssueAsync(Guid id, CancellationToken token)
    {
        var deleted = await _issueManager.DeleteIssueAsync(id, token);
        if (!deleted)
        {
            return IssueNotFound();
        }

        return NoContent();
    }

    private IActionResult IssueNotFound() => NotFound("Issue not found.");
}