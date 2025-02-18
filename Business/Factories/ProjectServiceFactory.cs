using Data.Entities;

namespace Business.Factories;

public static class ProjectServiceFactory
{
    public static ProjectServiceJunctionEntity Create(int projectId, int serviceId) => new()
    {
        ProjectId =projectId,
        ServiceId = serviceId,
    };
}