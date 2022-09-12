using Bismuth.Domain.Entities;
using Bismuth.Domain.Models;
using BismuthAPI.Abstractions.Repositories;
using BismuthAPI.Abstractions.Services;
using BismuthAPI.Contracts.Project;
using MapsterMapper;

namespace BismuthAPI.Services;

public sealed class ProjectManagerService : IProjectManagerService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectManagerService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<CreateProjectResponse> CreateProjectAsync(CreateProjectRequest request, CancellationToken token)
    {
        var project = _mapper.Map<Project>(request);
        var id = await _projectRepository.AddProjectAsync(project, token);

        var createdProject = await _projectRepository.GetProjectAsync(id, token);
        if (createdProject is null) throw new InvalidOperationException("Could not create project.");

        return _mapper.Map<CreateProjectResponse>(createdProject);
    }

    /// <inheritdoc />
    public async Task<GetProjectResponse?> GetProjectAsync(Guid id, CancellationToken token)
    {
        var project = await _projectRepository.GetProjectAsync(id, token);
        if (project is null)
        {
            return null;
        }

        return _mapper.Map<GetProjectResponse>(project);
    }

    /// <inheritdoc />
    public async Task<GetProjectsResponse> GetProjectsAsync(CancellationToken token)
    {
        var projects = await _projectRepository.GetProjectsAsync(token);

        return new GetProjectsResponse(_mapper.Map<IEnumerable<ProjectSummary>>(projects));
    }

    /// <inheritdoc />
    public async Task<UpdateProjectResponse?> UpdateProjectAsync(UpdateProjectRequest request, CancellationToken token)
    {
        var targetProject = await _projectRepository.GetProjectAsync(request.Id, token);
        if (targetProject is null)
        {
            return null;
        }

        var project = _mapper.Map(request, targetProject);
        await _projectRepository.UpdateProjectAsync(project, token);

        var updatedProject = await _projectRepository.GetProjectAsync(project.Id, token);
        if (updatedProject is null) throw new InvalidOperationException("Could not update project.");

        return _mapper.Map<UpdateProjectResponse>(updatedProject);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteProjectAsync(Guid id, CancellationToken token)
    {
        var targetProject = await _projectRepository.GetProjectAsync(id, token);
        if (targetProject is null)
        {
            return false;
        }

        await _projectRepository.DeleteProjectAsync(targetProject, token);

        var projects = await _projectRepository.GetProjectsAsync(token);
        if (projects.Any(p => p.Id == id)) throw new InvalidOperationException("Could not delete project.");

        return true;
    }
}
