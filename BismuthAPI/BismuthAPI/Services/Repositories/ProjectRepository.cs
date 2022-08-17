using BismuthAPI.Abstractions;
using BismuthAPI.Data.Models;

namespace BismuthAPI.Services.Repositories;

public sealed class ProjectRepository : BaseDbRepository, IProjectRepository {
    public ProjectRepository(DataContext dataContext)
        : base(dataContext) {}

    /// <inheritdoc />
    public async Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken token)
        => await DbContext.Projects.ToListAsync(token);

    /// <inheritdoc />
    public async Task<IEnumerable<Project>> AddProjectAsync(Project project, CancellationToken token)
    {
        DbContext.Projects.Add(project);
        await DbContext.SaveChangesAsync(token);

        return await GetProjectsAsync(token);
    }

    /// <inheritdoc />
    public async Task<Project?> GetProjectAsync(int id, CancellationToken token)
        => await DbContext.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, token);

    /// <inheritdoc />
    public async Task<Project?> UpdateProjectAsync(Project project, CancellationToken token)
    {
        DbContext.Projects.Update(project);
        await DbContext.SaveChangesAsync(token);

        return await GetProjectAsync(project.Id, token);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Project>> DeleteProjectAsync(Project project, CancellationToken token)
    {
        DbContext.Projects.Remove(project);
        await DbContext.SaveChangesAsync(token);

        return await GetProjectsAsync(token);
    }
}
