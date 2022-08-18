using BismuthAPI.Data.Models;

namespace BismuthAPI.Abstractions;

// TODO: add docs
public interface IIssueRepository {
    Task<Issue?> GetIssueAsync(int issueId, CancellationToken token);

    Task<IEnumerable<Issue>> GetProjectIssues(int projectId, CancellationToken token);

    Task<IEnumerable<Issue>> AddIssueAsync(Issue issue, CancellationToken token);

    Task<Issue?> UpdateIssueAsync(Issue issue, CancellationToken token);

    Task<IEnumerable<Issue>> DeleteIssueAsync(Issue issue, CancellationToken token);
}