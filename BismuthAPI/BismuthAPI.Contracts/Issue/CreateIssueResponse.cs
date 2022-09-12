namespace BismuthAPI.Contracts.Issue;

public sealed record CreateIssueResponse(
    Guid Id,
    string Title,
    string Description,
    Guid ProjectId);