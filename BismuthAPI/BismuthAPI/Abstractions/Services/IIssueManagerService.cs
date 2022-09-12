using BismuthAPI.Contracts.Issue;

namespace BismuthAPI.Abstractions.Services;

// TODO: add documentation
public interface IIssueManagerService
{
    Task<CreateIssueResponse?> CreateIssueAsync(CreateIssueRequest request, CancellationToken token);
    Task<GetIssueResponse?> GetIssueAsync(Guid id, CancellationToken token);
    Task<UpdateIssueResponse?> UpdateIssueAsync(UpdateIssueRequest request, CancellationToken token);
    Task<bool> DeleteIssueAsync(Guid id, CancellationToken token);
}