using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class ServiceService(IServiceRepository serviceRepository, IUnitTypeRepository unitTypeRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;
    private readonly IUnitTypeRepository _unitTypeRepository = unitTypeRepository;
    public async Task<IResponseResult> CreateServiceAsync(ServiceRegistrationForm form)
    {
        if (form == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var unitEntity = await _unitTypeRepository.GetAsync(x => x.UnitType == form.UnitType);
            if (unitEntity == null)
            {
                unitEntity = new UnitTypeEntity { UnitType = form.UnitType };
                await _unitTypeRepository.CreateAsync(unitEntity);
            }

            var serviceEntity = ServiceFactory.CreateEntity(form, unitEntity.Id);
            await _serviceRepository.CreateAsync(serviceEntity);

            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving service");
        }
    }

    public async Task<IResponseResult> DeleteServiceAsync(int id)
    {
        try
        {
            var entity = await _serviceRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Service not found");

            bool result = await _serviceRepository.DeleteAsync(x => x.Id == id);
            return result ? ResponseResult.Ok() : ResponseResult.Error("Unable to delete service");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error deleting service");
        }
    }

    public async Task<IResponseResult> GetAllServicesAsync()
    {
        try
        {
            var entites = await _serviceRepository.GetAllAsync();
            var services = entites.Select(ServiceFactory.CreateModel).ToList();
            return ResponseResult<IEnumerable<ServiceModel>>.Ok(services);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving services");
        }
    }

    public async Task<IResponseResult> GetServiceByIdAsync(int id)
    {
        try
        {
            var entity = await _serviceRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Service not found");

            var service = ServiceFactory.CreateModel(entity);
            return ResponseResult<ServiceModel>.Ok(service);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving service");
        }
    }

    public async Task<IResponseResult> UpdateServiceAsync(int id, ServiceRegistrationForm updateForm)
    {
        if (updateForm == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {
            var entityToUpdate = await _serviceRepository.GetAsync(x => x.Id == id);
            if (entityToUpdate == null)
                return ResponseResult.NotFound("Service not found");

            entityToUpdate = ServiceFactory.CreateEntity(updateForm, entityToUpdate.UnitId);
            var result = await _serviceRepository.UpdateAsync(x => x.Id == id, entityToUpdate);

            return result ? ResponseResult.Ok() : ResponseResult.Error("Unable to update service");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error updating service");
        }
    }
}