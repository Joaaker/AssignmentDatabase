using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class EmployeeFactory
{
    public static EmployeeEntity CreateEntity(EmployeeRegistrationForm registrationForm, int roleId) => new()
    {
        FirstName = registrationForm.FirstName,
        LastName = registrationForm.LastName,
        RoleId = roleId,
    };

    public static Employee CreateModel(EmployeeEntity entity) => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        RoleName = entity.Role.RoleName,
    };
}