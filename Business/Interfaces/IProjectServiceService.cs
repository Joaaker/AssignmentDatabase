using Business.Dtos;

namespace Business.Interfaces;

public interface IProjectServiceService
{
    //Task<IResponseResult> CreateProjectServiceAsync(ProjectServiceRegistrationForm registrationForm);
    Task<IResponseResult> GetAllProjectServicesAsync();
    Task<IResponseResult> GetProjectServicesByProjectId(int projectId);
    //Task<IResponseResult> UpdateProjectServiceAsync(int id, ProjectServiceRegistrationForm updateForm);
    Task<IResponseResult> DeleteProjectServiceAsync(int id);
}