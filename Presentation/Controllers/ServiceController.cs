using Business.Dtos;
using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceController(IServiceService serviceService) : Controller
{
    private readonly IServiceService _serviceService = serviceService;

    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] ServiceRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _serviceService.CreateServiceAsync(form);

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
    public async Task<IActionResult> GetAllServices()
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _serviceService.GetAllServicesAsync();
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
    public async Task<IActionResult> GetServiceById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _serviceService.GetServiceByIdAsync(id);
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
    public async Task<IActionResult> UpdateService(int id, ServiceRegistrationForm updateForm)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _serviceService.UpdateServiceAsync(id, updateForm);
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
    public async Task<IActionResult> DeleteServiceById(int id)
    {
        var response = await _serviceService.DeleteServiceAsync(id);
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