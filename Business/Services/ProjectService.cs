using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<IResponseResult> CreateProjectAsync(ProjectRegistrationForm form)
    {
        if (form == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var projectExist = await _projectRepository.AlreadyExistsAsync(x => x.Title == form.Title);
            if (projectExist == true)
                return ResponseResult.Error("Project with that name already exist");

            var projectEntity = ProjectFactory.CreateEntity(form);
            await _projectRepository.CreateAsync(projectEntity);
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error creating project");
        }
    }

    public async Task<IResponseResult> DeleteProjectAsync(int id)
    {
        try
        {
            var entity = await _projectRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Project not found");

            bool result = await _projectRepository.DeleteAsync(x => x.Id == id);
            return result ? ResponseResult.Ok() : ResponseResult.Error("Unable to delete project");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error deleting project");
        }
    }

    public async Task<IResponseResult> GetAllProjectsAsync()
    {
        try
        {
            var entites = await _projectRepository.GetAllAsync();
            var projects = entites.Select(ProjectFactory.CreateModel).ToList();
            return ResponseResult<IEnumerable<ProjectModel>>.Ok(projects);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving projects");
        }
    }

    public async Task<IResponseResult> GetProjectByIdAsync(int id)
    {
        try
        {
            var entity = await _projectRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Project not found");

            var project = ProjectFactory.CreateModel(entity);
            return ResponseResult<ProjectModel>.Ok(project);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving project");
        }
    }

    public async Task<IResponseResult> UpdateProjectAsync(int id, ProjectRegistrationForm updateForm)
    {
        if (updateForm == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var entityToUpdate = await _projectRepository.GetAsync(x => x.Id == id);
            if (entityToUpdate == null)
                return ResponseResult.NotFound("Project not found");

            entityToUpdate = ProjectFactory.CreateEntity(updateForm);
            var result = await _projectRepository.UpdateAsync(x => x.Id == id, entityToUpdate);

            return result ? ResponseResult.Ok() : ResponseResult.Error("Unable to update project");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error updating project");
        }
    }
}