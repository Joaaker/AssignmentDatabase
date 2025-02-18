using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

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

            await _projectRepository.BeginTransactionAsync();
            var projectEntity = ProjectFactory.CreateEntity(form);
            await _projectRepository.AddAsync(projectEntity);
            var saveResult = await _projectRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving project");


            await _projectRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error creating project :: {ex.Message}");
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

    //To do, gör så att det går at uppdatera projekt med nya services
    public async Task<IResponseResult> UpdateProjectAsync(int id, ProjectRegistrationForm updateForm)
    {
        if (updateForm == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var entityToUpdate = await _projectRepository.GetAsync(x => x.Id == id);
            if (entityToUpdate == null)
                return ResponseResult.NotFound("Project not found");

            await _projectRepository.BeginTransactionAsync();
            entityToUpdate = ProjectFactory.CreateEntity(updateForm);
            await _projectRepository.UpdateAsync(x => x.Id == id, entityToUpdate);
            var saveResult = await _projectRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving project");

            await _projectRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error updating project :: {ex.Message}");
        }
    }

    public async Task<IResponseResult> DeleteProjectAsync(int id)
    {
        try
        {
            var entity = await _projectRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Project not found");

            await _projectRepository.BeginTransactionAsync();
            await _projectRepository.DeleteAsync(x => x.Id == id);
            var saveResult = await _projectRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving project");

            await _projectRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting project :: {ex.Message}");
        }
    }
}