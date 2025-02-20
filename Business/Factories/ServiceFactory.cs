using System.Security.Authentication.ExtendedProtection;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ServiceFactory
{
    public static ServiceEntity CreateEntity(ServiceRegistrationForm registrationForm, int unitId) => new()
    {
        ServiceName = registrationForm.ServiceName,
        Price = registrationForm.Price,
        UnitId = unitId,
    };

    public static ServiceEntity CreateEntity(ServiceRegistrationForm registrationForm, int serviceId, int unitId) => new()
    {
        Id = serviceId,
        ServiceName = registrationForm.ServiceName,
        Price = registrationForm.Price,
        UnitId = unitId,
    };

    public static ServiceModel CreateModel(ServiceEntity entity) => new()
    {
        Id = entity.Id,
        ServiceName = entity.ServiceName,
        Price = entity.Price,
        UnitType = entity.UnitType.UnitType,
    };
}