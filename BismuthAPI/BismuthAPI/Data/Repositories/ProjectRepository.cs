using Bismuth.Domain.Entities;
using BismuthAPI.Abstractions.Repositories;

namespace BismuthAPI.Data.Repositories;

public sealed class ProjectRepository : BaseDbRepository, IProjectRepository
{
    public ProjectRepository(DataContext dataContext)
        : base(dataContext) { }

    /// <inheritdoc />
    public async Task<Guid> AddProjectAsync(Project project, CancellationToken token)
    {
        _dbContext.Projects.Add(project);
        await _dbContext.SaveChangesAsync(token);

        return project.Id;
    }

    /// <inheritdoc />
    public async Task<Project?> GetProjectAsync(Guid id, CancellationToken token)
        => await _dbContext.Projects.AsNoTracking().Include(p => p.Issues)
            .FirstOrDefaultAsync(p => p.Id == id, token);

    /// <inheritdoc />
    public async Task<IEnumerable<Project>> GetProjectsAsync(CancellationToken token)
        => await _dbContext.Projects.ToListAsync(token);

    /// <inheritdoc />
    public async Task UpdateProjectAsync(Project project, CancellationToken token)
    {
        _dbContext.Projects.Update(project);
        await _dbContext.SaveChangesAsync(token);
    }

    /// <inheritdoc />
    public async Task DeleteProjectAsync(Project project, CancellationToken token)
    {
        _dbContext.Projects.Remove(project);
        await _dbContext.SaveChangesAsync(token);
    }
}
