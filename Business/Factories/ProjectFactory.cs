using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ProjectFactory
{
    public static ProjectEntity CreateEntity(ProjectRegistrationForm registrationForm) => new()
    {
        Title = registrationForm.Title,
        Description = registrationForm.Description,
        StartDate = registrationForm.StartDate,
        EndDate = registrationForm.EndDate,
        StatusId = registrationForm.ProjectStatusId,
        CustomerId = registrationForm.CustomerId,
        ProjectManagerId = registrationForm.ProjectManagerId,
    };

    public static ProjectModel CreateModel(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        StatusType = entity.Status.StatusName,  
        CustomerName = entity.Customer.CustomerName,
        ProjectManagerName = entity.ProjectManager.FirstName + " " + entity.ProjectManager.LastName,
        //Denna kod är genererad av ChatGPT o3-mini - Den skapar en ServiceModel med alla korrekta värden
        Services = entity.ProjectServices
        .Select(ps => new ServiceModel
        {
            Id = ps.Service.Id,
            ServiceName = ps.Service.ServiceName,
            Price = ps.Service.Price,
            UnitType = ps.Service.UnitType.UnitType 
        })
        .ToList()
    };
}
