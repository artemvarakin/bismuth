using BismuthAPI.Abstractions;
using BismuthAPI.Data.Models;

namespace BismuthAPI.Services.Repositories;

public sealed class ProjectRepository : BaseDbRepository, IProjectRepository {

    public ProjectRepository(DataContext dataContext) : base(dataContext) { }

    /// <inheritdoc />
    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        return await DbContext.Projects.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Project>> AddProjectAsync(Project project)
    {
        DbContext.Projects.Add(project);
        await DbContext.SaveChangesAsync();

        return await DbContext.Projects.ToListAsync();
    }

    /// <inheritdoc />
    public Task<Project> UpdateProjectAsync(Project project)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task DeleteProjectAsync(int id)
    {
        throw new NotImplementedException();
    }
}