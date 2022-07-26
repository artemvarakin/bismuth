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
    public async Task<ActionResult<IEnumerable<Project>>> GetProjectsAsync() {
        var projects = await _projectRepository.GetProjectsAsync();
        return Ok(projects);
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<Project>>> AddProjectAsync(Project project) {
        var projects = await _projectRepository.AddProjectAsync(project);
        return Ok(projects);
    }
}