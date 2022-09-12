namespace BismuthAPI.Contracts.Issue;

public sealed record CreateIssueRequest(string Title, string Description, Guid ProjectId);