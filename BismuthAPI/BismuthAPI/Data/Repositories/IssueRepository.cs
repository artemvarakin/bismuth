using Bismuth.Domain.Entities;
using BismuthAPI.Abstractions.Repositories;

namespace BismuthAPI.Data.Repositories;

public sealed class IssueRepository : BaseDbRepository, IIssueRepository
{
    public IssueRepository(DataContext dataContext)
        : base(dataContext) { }

    /// <inheritdoc />
    public async Task<Issue?> GetIssueAsync(Guid issueId, CancellationToken token)
        => await _dbContext.Issues.AsNoTracking().FirstOrDefaultAsync(i => i.Id == issueId, token);

    /// <inheritdoc />
    public async Task<Guid> AddIssueAsync(Issue issue, CancellationToken token)
    {
        _dbContext.Issues.Add(issue);
        await _dbContext.SaveChangesAsync(token);

        return issue.Id;
    }

    /// <inheritdoc />
    public async Task UpdateIssueAsync(Issue issue, CancellationToken token)
    {
        _dbContext.Issues.Update(issue);
        await _dbContext.SaveChangesAsync(token);
    }

    /// <inheritdoc />
    public async Task DeleteIssueAsync(Issue issue, CancellationToken token)
    {
        _dbContext.Remove(issue);
        await _dbContext.SaveChangesAsync(token);
    }
}