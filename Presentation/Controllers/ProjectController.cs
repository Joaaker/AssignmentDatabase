using Business.Dtos;
using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProjectController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] ProjectRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _projectService.CreateProjectAsync(form);

        return response.StatusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response.ErrorMessage),
            409 => Conflict(response.ErrorMessage),
            500 => Problem(response.ErrorMessage),
            _ => Problem("Something went wrong!"),
        };
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _projectService.GetAllProjectsAsync();
        return response.StatusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response.ErrorMessage),
            409 => Conflict(response.ErrorMessage),
            500 => Problem(response.ErrorMessage),
            _ => Problem("Something went wrong!"),
        };
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _projectService.GetProjectByIdAsync(id);
        return response.StatusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response.ErrorMessage),
            409 => Conflict(response.ErrorMessage),
            500 => Problem(response.ErrorMessage),
            _ => Problem("Something went wrong!"),
        };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, ProjectRegistrationForm updateForm)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _projectService.UpdateProjectAsync(id, updateForm);
        return response.StatusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response.ErrorMessage),
            409 => Conflict(response.ErrorMessage),
            500 => Problem(response.ErrorMessage),
            _ => Problem("Something went wrong!"),
        };
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectById(int id)
    {
        var response = await _projectService.DeleteProjectAsync(id);
        return response.StatusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response.ErrorMessage),
            409 => Conflict(response.ErrorMessage),
            500 => Problem(response.ErrorMessage),
            _ => Problem("Something went wrong!"),
        };
    }
}