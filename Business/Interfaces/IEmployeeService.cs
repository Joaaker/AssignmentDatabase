using Business.Dtos;

namespace Business.Interfaces;

public interface IEmployeeService
{
    Task<IResponseResult> CreateEmployeeAsync(EmployeeRegistrationForm registrationForm);
    Task<IResponseResult> GetEmployeesAsync();
    Task<IResponseResult> GetEmployeeByIdAsync(int id);
    Task<IResponseResult> UpdateEmployeeAsync(int id, EmployeeRegistrationForm updateForm);
    Task<IResponseResult> DeleteEmployeeAsync(int id);
}
