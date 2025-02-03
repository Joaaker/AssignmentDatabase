using Business.Dtos;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<IResponseResult> CreateProjectAsync(ProjectRegistrationForm registrationForm);
    Task<IResponseResult> GetAllProjectsAsync();
    Task<IResponseResult> GetProjectByIdAsync(int id);
    Task<IResponseResult> UpdateProjectAsync(int id, ProjectRegistrationForm updateForm);
    Task<IResponseResult> DeleteProjectAsync(int id);
}