using BismuthAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BismuthAPI.Abstractions;

// TODO: add docs
public interface IProjectRepository {
    Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken token);

    Task<IEnumerable<Project>> AddProjectAsync(Project project, CancellationToken token);

    Task<Project?> GetProjectAsync(int id, CancellationToken token);

    Task<Project?> UpdateProjectAsync(Project project, CancellationToken token);

    Task<IEnumerable<Project>> DeleteProjectAsync(Project project, CancellationToken token);
}