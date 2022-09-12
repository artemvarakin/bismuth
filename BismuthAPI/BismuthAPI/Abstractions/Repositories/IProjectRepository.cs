using Bismuth.Domain.Entities;

namespace BismuthAPI.Abstractions.Repositories;

// TODO: add documentation
public interface IProjectRepository
{
    Task<Guid> AddProjectAsync(Project project, CancellationToken token);
    Task<Project?> GetProjectAsync(Guid id, CancellationToken token);
    Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken token);
    Task UpdateProjectAsync(Project project, CancellationToken token);
    Task DeleteProjectAsync(Project id, CancellationToken token);
}