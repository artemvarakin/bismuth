namespace BismuthAPI.Contracts.Project;

public sealed record CreateProjectRequest(
    string Name,
    string? Description);