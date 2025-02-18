using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class ProjectServiceService(IProjectServiceRepository projectServiceRepository) : IProjectServiceService
{
    private readonly IProjectServiceRepository _projectServiceRepository = projectServiceRepository;

    //public async Task<IResponseResult> CreateProjectServiceAsync(ProjectServiceRegistrationForm form)
    //{
    //    if (form == null)
    //        return ResponseResult.BadRequest("Invalid form");
    //    try
    //    {
    //        await _projectServiceRepository.BeginTransactionAsync();
    //        var projectServiceJunctionEntity = ProjectServiceFactory.Create(form);
    //        await _projectServiceRepository.AddAsync(projectServiceJunctionEntity);
    //        var saveResult = await _projectServiceRepository.SaveAsync();
    //        if (saveResult == false)
    //            throw new Exception("Error saving ProjectService");

    //        await _projectServiceRepository.CommitTransactionAsync();
    //        return ResponseResult.Ok();
    //    }
    //    catch (Exception ex)
    //    {
    //        await _projectServiceRepository.RollbackTransactionAsync();
    //        Debug.WriteLine(ex.Message);
    //        return ResponseResult.Error($"Error creating project :: {ex.Message}");
    //    }
    //}

    public Task<IResponseResult> DeleteProjectServiceAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IResponseResult> GetAllProjectServicesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IResponseResult> GetProjectServicesByProjectId(int id)
    {
        throw new NotImplementedException();
    }

    //public Task<IResponseResult> UpdateProjectServiceAsync(int id, ProjectServiceRegistrationForm updateForm)
    //{
    //    throw new NotImplementedException();
    //}
}