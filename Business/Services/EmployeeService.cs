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
            await _employeeRepository.BeginTransactionAsync();
            if (roleEntity == null)
            {
                roleEntity = new RoleEntity { RoleName = form.RoleName };
                await _roleRepository.AddAsync(roleEntity);
                bool saveRoleResult = await _roleRepository.SaveAsync();
                if (saveRoleResult == false)
                    throw new Exception("Error saving role");
            }

            var employeeEntity = EmployeeFactory.CreateEntity(form, roleEntity.Id);
            await _employeeRepository.AddAsync(employeeEntity);
            bool saveResult = await _employeeRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving employee");

            await _employeeRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _employeeRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error creating employee");
        }
    }

    public async Task<IResponseResult> DeleteEmployeeAsync(int id)
    {
        try
        {
            var entity = await _employeeRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Employee not found");

            await _employeeRepository.BeginTransactionAsync();
            await _employeeRepository.DeleteAsync(x => x.Id == id);
            bool saveResult = await _employeeRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving");

            await _employeeRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _employeeRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting employee :: {ex.Message}");
        }
    }

    public async Task<IResponseResult> GetEmployeeByIdAsync(int id)
    {
        try
        {
            var entity = await _employeeRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Employee not found");

            var employee = EmployeeFactory.CreateModel(entity);
            return ResponseResult<Employee>.Ok(employee);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving employee");
        }
    }

    public async Task<IResponseResult> GetAllEmployeesAsync()
    {
        try
        {
            var entites = await _employeeRepository.GetAllAsync();
            var employees = entites.Select(EmployeeFactory.CreateModel).ToList();
            return ResponseResult<IEnumerable<Employee>>.Ok(employees);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving employees");
        }
    }

    public async Task<IResponseResult> UpdateEmployeeAsync(int id, EmployeeRegistrationForm updateForm)
    {
        if (updateForm == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var entityToUpdate = await _employeeRepository.GetAsync(x => x.Id == id);
            if (entityToUpdate == null)
                return ResponseResult.NotFound("Employee not found");

            await _employeeRepository.BeginTransactionAsync();
            entityToUpdate = EmployeeFactory.CreateEntity(updateForm, entityToUpdate.Id, entityToUpdate.RoleId);

            await _employeeRepository.UpdateAsync(x => x.Id == id, entityToUpdate);

            bool saveResult = await _employeeRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving");

            await _employeeRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _employeeRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error updating employee :: {ex.Message}");
        }
    }
}