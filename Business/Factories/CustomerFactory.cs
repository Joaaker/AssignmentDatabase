using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class CustomerFactory
{
    public static CustomerEntity CreateEntity(CustomerRegistrationForm registrationForm) => new()
    {
        CustomerName = registrationForm.CustomerName
    };

    public static Customer CreateModel(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName,
    };
}