using Business.Dtos;
using System.Diagnostics;
using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CustomerController(ICustomerService customerService) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService;

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _customerService.CreateCustomerAsync(form);

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
    public async Task<IActionResult> GetAllCustomers()
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _customerService.GetAllCustomersAsync();
        return response.StatusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response.ErrorMessage),
            409 => Conflict(response.ErrorMessage),
            500 => Problem(response.ErrorMessage),
             _  => Problem("Something went wrong!"),
        };
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _customerService.GetCustomerByIdAsync(id);
        return response.StatusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response.ErrorMessage),
            409 => Conflict(response.ErrorMessage),
            500 => Problem(response.ErrorMessage),
             _  => Problem("Something went wrong!"),
        };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, CustomerRegistrationForm updateForm)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var response = await _customerService.UpdateCustomerAsync(id, updateForm);
        return response.StatusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response.ErrorMessage),
            409 => Conflict(response.ErrorMessage),
            500 => Problem(response.ErrorMessage),
             _  => Problem("Something went wrong!"),
        };
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerById(int id)
    {
        var response = await _customerService.DeleteCustomerAsync(id);
        return response.StatusCode switch
        {
            200 => Ok(response),
            400 => BadRequest(response.ErrorMessage),
            409 => Conflict(response.ErrorMessage),
            500 => Problem(response.ErrorMessage),
             _  => Problem("Something went wrong!"),
        };
    }
}