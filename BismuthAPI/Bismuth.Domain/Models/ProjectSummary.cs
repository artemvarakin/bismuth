namespace Bismuth.Domain.Models;

public sealed record ProjectSummary(
    Guid Id,
    string Name,
    string? Description);