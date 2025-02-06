using Business.Interfaces;
using Business.Services;
using Business.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class EmployeeController(IEmployeeService employeeService) : Controller
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _employeeService.CreateEmployeeAsync(form);

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
    public async Task<IActionResult> GetAllEmployees()
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _employeeService.GetAllEmployeesAsync();
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
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _employeeService.GetEmployeeByIdAsync(id);
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
    public async Task<IActionResult> UpdateEmployee(int id, EmployeeRegistrationForm updateForm)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _employeeService.UpdateEmployeeAsync(id, updateForm);
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
    public async Task<IActionResult> DeleteEmployeeById(int id)
    {
        var response = await _employeeService.DeleteEmployeeAsync(id);
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