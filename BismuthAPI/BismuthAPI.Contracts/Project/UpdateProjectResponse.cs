namespace BismuthAPI.Contracts.Project;

public sealed record UpdateProjectResponse(
    Guid Id,
    string Name,
    string? Description);