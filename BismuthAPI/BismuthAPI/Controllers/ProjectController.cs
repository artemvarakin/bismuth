using BismuthAPI.Abstractions;
using BismuthAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BismuthAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ProjectController : ControllerBase {
    private readonly IProjectRepository _projectRepository;

    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjectsAsync(CancellationToken token)
    {
        var projects = await _projectRepository.GetProjectsAsync(token);
        return Ok(projects);
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<Project>>> AddProjectAsync(Project project, CancellationToken token)
    {
        var projects = await _projectRepository.AddProjectAsync(project, token);
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProjectAsync(int id, CancellationToken token)
    {
        var project = await _projectRepository.GetProjectAsync(id, token);
        if (project is null) {
            return NotFound("Project not found.");
        }

        return Ok(project);
    }

    [HttpPut]
    public async Task<ActionResult<Project>> UpdateProjectAsync(Project project, CancellationToken token)
    {
        var targetProject = await _projectRepository.GetProjectAsync(project.Id, token);
        if (targetProject is null) {
            return NotFound("Project not found.");
        }

        var updatedProject = await _projectRepository.UpdateProjectAsync(project, token);
        return Ok(updatedProject);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProjectAsync(int id, CancellationToken token)
    {
        var targetProject = await _projectRepository.GetProjectAsync(id, token);
        if (targetProject is null) {
            return NotFound("Project not found.");
        }

        var availableProjects = await _projectRepository.DeleteProjectAsync(targetProject, token);
        return Ok(availableProjects);
    }
}