namespace BismuthAPI.Contracts.Issue;

public sealed record GetIssueResponse(
    Guid Id,
    string Title,
    string Description,
    Guid ProjectId);