using Bismuth.Domain.Models;

namespace BismuthAPI.Contracts.Project;

public sealed record GetProjectResponse(
    Guid Id,
    string Name,
    string? Description,
    IEnumerable<IssueSummary> Issues);