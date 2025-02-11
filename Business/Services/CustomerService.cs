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

            await _customerRepository.BeginTransactionAsync();
            var customerEntity = CustomerFactory.CreateEntity(form);
            await _customerRepository.AddAsync(customerEntity);
            bool saveResult = await _customerRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving");

            await _customerRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error creating customer :: {ex.Message}");
        }
    }

    public async Task<IResponseResult> GetAllCustomersAsync()
    {
        try
        {
            var entites = await _customerRepository.GetAllAsync();
            var customers = entites.Select(CustomerFactory.CreateModel).ToList();
            return ResponseResult<IEnumerable<Customer>>.Ok(customers);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving customers");
        }
    }

    public async Task<IResponseResult> GetCustomerByIdAsync(int id)
    {
        try
        {
            var entity = await _customerRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Customer not found");

            var customer = CustomerFactory.CreateModel(entity);
            return ResponseResult<Customer>.Ok(customer);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving customer");
        }
    }

    public async Task<IResponseResult> UpdateCustomerAsync(int id, CustomerRegistrationForm updateForm)
    {
        if (updateForm == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var entityToUpdate = await _customerRepository.GetAsync(x => x.Id == id);
            if (entityToUpdate == null)
                return ResponseResult.NotFound("Customer not found");

            await _customerRepository.BeginTransactionAsync();
            entityToUpdate = CustomerFactory.CreateEntity(updateForm);
            await _customerRepository.UpdateAsync(x => x.Id == id, entityToUpdate);
            bool saveResult = await _customerRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving");

            await _customerRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error updating customer :: {ex.Message}");
        }
    }
    public async Task<IResponseResult> DeleteCustomerAsync(int id)
    {
        try
        {
            var entity = await _customerRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Customer not found");

            await _customerRepository.BeginTransactionAsync();
            await _customerRepository.DeleteAsync(x => x.Id == id);
            bool saveResult = await _customerRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving");

            await _customerRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting customer :: {ex.Message}");
        }
    }
}