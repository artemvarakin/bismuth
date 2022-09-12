using Bismuth.Domain.Models;

namespace BismuthAPI.Contracts.Project;

public sealed record GetProjectsResponse(IEnumerable<ProjectSummary> Projects);