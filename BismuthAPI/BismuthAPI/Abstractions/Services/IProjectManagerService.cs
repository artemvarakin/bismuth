using BismuthAPI.Contracts.Project;

namespace BismuthAPI.Abstractions.Services;

// TODO: add documentation
public interface IProjectManagerService
{
    Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest request, CancellationToken token);
    Task<GetProjectResponse?> GetProjectAsync(Guid id, CancellationToken token);
    Task<GetProjectsResponse> GetProjectsAsync(CancellationToken token);
    Task<UpdateProjectResponse?> UpdateProjectAsync(UpdateProjectRequest request, CancellationToken token);
    Task<bool> DeleteProjectAsync(Guid id, CancellationToken token);
}