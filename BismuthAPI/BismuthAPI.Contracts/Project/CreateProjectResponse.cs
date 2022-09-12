namespace BismuthAPI.Contracts.Project;

public sealed record CreateProjectResponse(
    Guid Id,
    string Name,
    string? Description);