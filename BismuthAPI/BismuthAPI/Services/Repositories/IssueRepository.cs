using BismuthAPI.Abstractions;
using BismuthAPI.Data.Models;

namespace BismuthAPI.Services.Repositories;

public sealed class IssueRepository : BaseDbRepository, IIssueRepository
{
    public IssueRepository(DataContext dataContext)
        : base(dataContext) {}

    /// <inheritdoc />
    public async Task<Issue?> GetIssueAsync(int issueId, CancellationToken token)
        => await DbContext.Issues.AsNoTracking().FirstOrDefaultAsync(i => i.Id == issueId, token);

    /// <inheritdoc />
    public async Task<IEnumerable<Issue>> GetProjectIssues(int projectId, CancellationToken token)
        => await DbContext.Issues.Where(i => i.ProjectId == projectId).ToListAsync(token);

    /// <inheritdoc />
    public async Task<IEnumerable<Issue>> AddIssueAsync(Issue issue, CancellationToken token)
    {
        DbContext.Issues.Add(issue);
        await DbContext.SaveChangesAsync(token);

        return await GetProjectIssues(issue.ProjectId, token);
    }

    /// <inheritdoc />
    public async Task<Issue?> UpdateIssueAsync(Issue issue, CancellationToken token)
    {
        DbContext.Issues.Update(issue);
        await DbContext.SaveChangesAsync(token);

        return await GetIssueAsync(issue.Id, token);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Issue>> DeleteIssueAsync(Issue issue, CancellationToken token)
    {
        DbContext.Issues.Remove(issue);
        await DbContext.SaveChangesAsync(token);

        return await GetProjectIssues(issue.ProjectId, token);
    }
}