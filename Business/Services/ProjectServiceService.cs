using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class ProjectServiceService(IProjectServiceRepository projectServiceRepository) : IProjectServiceService
{
    private readonly IProjectServiceRepository _projectServiceRepository = projectServiceRepository;

    public async Task<IResponseResult> DeleteProjectServiceAsync(int projectId, int serviceId)
    {
        //Transactions are handled in ProjectService.UpdateProjectAsync
        try
        {
            var projectServiceJunctionEntity = await _projectServiceRepository.GetAsync(
                x => x.ProjectId == projectId && x.ServiceId == serviceId);
            if (projectServiceJunctionEntity == null)
                return ResponseResult.NotFound("ProjectService not found");

            await _projectServiceRepository.DeleteAsync(
                x => x.ProjectId == projectId && x.ServiceId == serviceId);
            var saveResult = await _projectServiceRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error occurred while saving the deletion.");

            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting ProjectService :: {ex.Message}");
        }
    }


    public async Task<IResponseResult> UpdateProjectServicesAsync(int projectId, List<int> currentServiceIds, List<int> newServiceIds)
    {
        if (currentServiceIds == null || newServiceIds == null)
            return ResponseResult.BadRequest("Invalid update ProjectService input");
        //Transactions are handled in ProjectService.UpdateProjectAsync
        try
        {
            if (currentServiceIds.SequenceEqual(newServiceIds))
                return ResponseResult.Ok();

            var toRemove = currentServiceIds.Except(newServiceIds).ToList();
            var toAdd = newServiceIds.Except(currentServiceIds).ToList();

            foreach (int serviceId in toRemove)
            {
                var deleteResponse = DeleteProjectServiceAsync(projectId, serviceId);
                if (deleteResponse.Result.Success == false)
                    throw new Exception("Error deleting ProjectService");
            }

            foreach (var serviceId in toAdd)
            {
                var newProjectServiceEntity = ProjectServiceFactory.Create(projectId, serviceId);
                await _projectServiceRepository.AddAsync(newProjectServiceEntity);
                var psAddSaveResult = await _projectServiceRepository.SaveAsync();
                if (psAddSaveResult == false)
                    throw new Exception("Error saving updated ProjectService");
            }

            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error($"Error deleting ProjectService :: {ex.Message}");
        }
    }
}