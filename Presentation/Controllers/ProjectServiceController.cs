using Business.Dtos;
using Business.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProjectServiceController(IProjectServiceService ProjectServiceService) : Controller
{
    private readonly IProjectServiceService _ProjectServiceService = ProjectServiceService;

    [HttpPost]
    public async Task<IActionResult> CreateProjectService([FromBody] ProjectServiceRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _ProjectServiceService.CreateProjectServiceAsync(form);

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
    public async Task<IActionResult> GetAllProjectServices()
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _ProjectServiceService.GetAllProjectServicesAsync();
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
    public async Task<IActionResult> GetProjectServicesByProjectId(int projectId)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _ProjectServiceService.GetProjectServicesByProjectId(projectId);
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
    public async Task<IActionResult> UpdateProjectService(int projectId, ProjectServiceRegistrationForm updateForm)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _ProjectServiceService.UpdateProjectServiceAsync(projectId, updateForm);
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
    public async Task<IActionResult> DeleteProjectServiceById(int id)
    {
        var response = await _ProjectServiceService.DeleteProjectServiceAsync(id);
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