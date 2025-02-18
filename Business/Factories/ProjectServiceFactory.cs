using Business.Dtos;
using Data.Entities;

namespace Business.Factories;

public static class ProjectServiceFactory
{
    public static ProjectServiceJunctionEntity Create(ProjectServiceRegistrationForm form) => new()
    {
        ProjectId = form.ProjectId,
        ServiceId = form.ServiceId,
    };
}