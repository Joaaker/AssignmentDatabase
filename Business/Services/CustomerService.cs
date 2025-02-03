using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;
public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<IResponseResult> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        if (form == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var customerExist = await _customerRepository.AlreadyExistsAsync(x => x.CustomerName == form.CustomerName);
            if (customerExist == true)
                return ResponseResult.Error("Customer with that name already exist");

            var customerEntity = CustomerFactory.CreateEntity(form);
            await _customerRepository.CreateAsync(customerEntity);
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error creating customer");
        }
    }

    public async Task<IResponseResult> DeleteCustomerAsync(int id)
    {
        var entity = await _customerRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return ResponseResult.NotFound("Customer not found");

        var result = await _customerRepository.DeleteAsync(x => x.Id == id);
        return result ? ResponseResult.Ok() : ResponseResult.Error("Unable to delete customer");
    }

    public async Task<IResponseResult> GetAllCustomersAsync()
    {
        var entites = await _customerRepository.GetAllAsync();
        var customers = entites.Select(CustomerFactory.CreateModel).ToList();
        return ResponseResult<IEnumerable<Customer>>.Ok(customers);
    }

    public async Task<IResponseResult> GetCustomerByIdAsync(int id)
    {
        var entity = await _customerRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return ResponseResult.NotFound("Customer not found");

        var customer = CustomerFactory.CreateModel(entity);
        return ResponseResult<Customer>.Ok(customer);
    }

    public async Task<IResponseResult> UpdateCustomerAsync(int id, CustomerRegistrationForm updateForm)
    {
        var entityToUpdate = await _customerRepository.GetAsync(x => x.Id == id);
        if (entityToUpdate == null)
            return ResponseResult.NotFound("Customer not found");

        entityToUpdate = CustomerFactory.CreateEntity(updateForm);
        var result = await _customerRepository.UpdateAsync(x => x.Id == id, entityToUpdate);

        return result ? ResponseResult.Ok() : ResponseResult.Error("Unable to update customer");
    }
}