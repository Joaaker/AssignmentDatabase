using Business.Dtos;

namespace Business.Interfaces;

public interface IProjectServiceService
{
    Task<IResponseResult> UpdateProjectServicesAsync(int projectId, List<int> currentServiceIds, List<int> newServiceIds);
    Task<IResponseResult> DeleteProjectServiceAsync(int projectId, int serviceId);
}