using Business.Dtos;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<IResponseResult> CreateCustomerAsync(CustomerRegistrationForm registrationForm);
    Task<IResponseResult> GetAllCustomersAsync();
    Task<IResponseResult> GetCustomerByIdAsync(int id);
    Task<IResponseResult> UpdateCustomerAsync(int id, CustomerRegistrationForm updateForm);
    Task<IResponseResult> DeleteCustomerAsync(int id);
}
