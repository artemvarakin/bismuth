namespace BismuthAPI.Contracts.Issue;

public sealed record UpdateIssueRequest(
    Guid Id,
    string Title,
    string Description);