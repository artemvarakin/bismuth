using Bismuth.Domain.Entities;

namespace BismuthAPI.Abstractions.Repositories;

// TODO: add documentation
public interface IIssueRepository
{
    Task<Issue?> GetIssueAsync(Guid id, CancellationToken token);

    Task<Guid> AddIssueAsync(Issue issue, CancellationToken token);

    Task UpdateIssueAsync(Issue issue, CancellationToken token);

    Task DeleteIssueAsync(Issue issue, CancellationToken token);
}