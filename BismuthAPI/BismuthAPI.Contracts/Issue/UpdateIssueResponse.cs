namespace BismuthAPI.Contracts.Issue;

public sealed record UpdateIssueResponse(
    Guid Id,
    string Title,
    string Description);