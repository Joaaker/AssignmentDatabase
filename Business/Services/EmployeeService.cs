using System.Data;
using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class EmployeeService(IEmployeeRepository employeeRepository, IRoleRepository roleRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<IResponseResult> CreateEmployeeAsync(EmployeeRegistrationForm form)
    {
        if (form == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var roleEntity = await _roleRepository.GetAsync(r => r.RoleName == form.RoleName);
            if(roleEntity == null)
            {
                var newRole = new RoleEntity { RoleName = form.RoleName };
                await _roleRepository.CreateAsync(newRole);
                var employeeEntity = EmployeeFactory.CreateEntity(form, newRole.Id);
                var result = await _employeeRepository.CreateAsync(employeeEntity);
                return ResponseResult.Ok();
            }
            else
            {
                var employeeEntity = EmployeeFactory.CreateEntity(form, roleEntity.Id);
                var result = await _employeeRepository.CreateAsync(employeeEntity);
                return ResponseResult.Ok();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error creating employee");
        }
    }

    public Task<IResponseResult> DeleteEmployeeAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IResponseResult> GetEmployeeByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IResponseResult> GetEmployeesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IResponseResult> UpdateEmployeeAsync(int id, EmployeeRegistrationForm updateForm)
    {
        throw new NotImplementedException();
    }
}
