using Bismuth.Domain.Entities;
using BismuthAPI.Abstractions.Repositories;
using BismuthAPI.Abstractions.Services;
using BismuthAPI.Contracts.Issue;
using MapsterMapper;

namespace BismuthAPI.Services;

public sealed class IssueManagerService : IIssueManagerService
{
    private readonly IIssueRepository _issueRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public IssueManagerService(
        IIssueRepository issueRepository,
        IProjectRepository projectRepository,
        IMapper mapper)
    {
        _issueRepository = issueRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<CreateIssueResponse?> CreateIssueAsync(CreateIssueRequest request, CancellationToken token)
    {
        var project = await _projectRepository.GetProjectAsync(request.ProjectId, token);
        if (project is null)
        {
            return null;
        }

        var issue = _mapper.Map<Issue>(request);
        var id = await _issueRepository.AddIssueAsync(issue, token);

        var createdIssue = await _issueRepository.GetIssueAsync(id, token);
        if (createdIssue is null) throw new InvalidOperationException("Could not create issue.");

        return _mapper.Map<CreateIssueResponse>(createdIssue);
    }

    /// <inheritdoc />
    public async Task<GetIssueResponse?> GetIssueAsync(Guid id, CancellationToken token)
    {
        var issue = await _issueRepository.GetIssueAsync(id, token);
        if (issue is null)
        {
            return null;
        }

        return _mapper.Map<GetIssueResponse>(issue);
    }

    /// <inheritdoc />
    public async Task<UpdateIssueResponse?> UpdateIssueAsync(UpdateIssueRequest request, CancellationToken token)
    {
        var targetIssue = await _issueRepository.GetIssueAsync(request.Id, token);
        if (targetIssue is null)
        {
            return null;
        }

        var issue = _mapper.Map(request, targetIssue);

        await _issueRepository.UpdateIssueAsync(issue, token);

        var updatedIssue = await _issueRepository.GetIssueAsync(issue.Id, token);
        if (updatedIssue is null) throw new InvalidOperationException("Could not update issue.");

        return _mapper.Map<UpdateIssueResponse>(updatedIssue);
    }

    public async Task<bool> DeleteIssueAsync(Guid id, CancellationToken token)
    {
        var issue = await _issueRepository.GetIssueAsync(id, token);
        if (issue is null)
        {
            return false;
        }

        await _issueRepository.DeleteIssueAsync(issue, token);

        var project = await _projectRepository.GetProjectAsync(issue.ProjectId, token);
        if (project!.Issues.Any(i => i.Id == id)) throw new InvalidOperationException("Could not delete issue.");

        return true;
    }
}