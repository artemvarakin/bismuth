namespace Bismuth.Domain.Entities;

public sealed class Issue
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public Guid ProjectId { get; init; }
}