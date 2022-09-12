using BismuthAPI.Abstractions.Services;
using BismuthAPI.Contracts.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BismuthAPI.Controllers;

[Authorize]
public sealed class ProjectsController : ApiController
{
    private readonly IProjectManagerService _projectManager;

    public ProjectsController(IProjectManagerService projectManager)
    {
        _projectManager = projectManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProjectAsync(CreateProjectRequest request, CancellationToken token)
    {
        var project = await _projectManager.CreateProjectAsync(request, token);

        return CreatedAtAction(nameof(GetProjectAsync), new { id = project.Id }, project);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProjectAsync(Guid id, CancellationToken token)
    {
        var project = await _projectManager.GetProjectAsync(id, token);
        if (project is null)
        {
            return ProjectNotFound();
        }

        return Ok(project);
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectsAsync(CancellationToken token)
    {
        var projects = await _projectManager.GetProjectsAsync(token);

        return Ok(projects);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProjectAsync(UpdateProjectRequest request, CancellationToken token)
    {
        var project = await _projectManager.UpdateProjectAsync(request, token);
        if (project is null)
        {
            return ProjectNotFound();
        }

        return Ok(project);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProjectAsync(Guid id, CancellationToken token)
    {
        var deleted = await _projectManager.DeleteProjectAsync(id, token);
        if (!deleted)
        {
            return ProjectNotFound();
        }

        return NoContent();
    }

    private IActionResult ProjectNotFound() => NotFound("Project not found.");
}