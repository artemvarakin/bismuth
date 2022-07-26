using BismuthAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BismuthAPI.Abstractions;

// TODO: add docs
public interface IProjectRepository {
    Task<IEnumerable<Project>> GetProjectsAsync();

    Task<IEnumerable<Project>> AddProjectAsync(Project project);

    Task<Project> UpdateProjectAsync(Project project);

    Task DeleteProjectAsync(int id);
}