namespace BismuthAPI.Contracts.Project;

public sealed record UpdateProjectRequest(
    Guid Id,
    string Name,
    string? Description);