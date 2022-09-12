namespace Bismuth.Domain.Entities;

public sealed class Project
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public IList<Issue> Issues { get; init; } = null!;
}