using Business.Dtos;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CustomerController(CustomerService customerService) : ControllerBase
{
    private readonly CustomerService _customerService = customerService;

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
            _ => Problem("Something went wrong!"),
        };

    }
}

