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

        await _serviceRepository.BeginTransactionAsync();
        try
        {
            var unitEntity = await _unitTypeRepository.GetAsync(x => x.UnitType == form.UnitType);
            if (unitEntity == null)
            {
                unitEntity = new UnitTypeEntity { UnitType = form.UnitType };
                await _unitTypeRepository.AddAsync(unitEntity);
                await _unitTypeRepository.SaveAsync();
            }

            var serviceEntity = ServiceFactory.CreateEntity(form, unitEntity.Id);
            await _serviceRepository.AddAsync(serviceEntity);
            await _serviceRepository.SaveAsync();

            await _serviceRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _serviceRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving service");
        }
    }

    public async Task<IResponseResult> DeleteServiceAsync(int id)
    {
        await _serviceRepository.BeginTransactionAsync();

        try
        {
            var entity = await _serviceRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Service not found");

            bool result = await _serviceRepository.DeleteAsync(x => x.Id == id);
            if (result)
                await _serviceRepository.SaveAsync();
            else
            {
                await _serviceRepository.RollbackTransactionAsync();
                return ResponseResult.Error("Unable to delete service");
            }

            await _serviceRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _serviceRepository.RollbackTransactionAsync();
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

        await _serviceRepository.BeginTransactionAsync();

        try
        {
            var entityToUpdate = await _serviceRepository.GetAsync(x => x.Id == id);
            if (entityToUpdate == null)
            {
                await _serviceRepository.RollbackTransactionAsync();
                return ResponseResult.NotFound("Service not found");
            }

            entityToUpdate = ServiceFactory.CreateEntity(updateForm, entityToUpdate.UnitId);
            var result = await _serviceRepository.UpdateAsync(x => x.Id == id, entityToUpdate);
            if (result)
                await _serviceRepository.SaveAsync();
            else
            {
                await _serviceRepository.RollbackTransactionAsync();
                return ResponseResult.Error("Unable to update service");
            }

            await _serviceRepository.CommitTransactionAsync();
            return ResponseResult.Ok();
        }
        catch (Exception ex)
        {
            await _serviceRepository.RollbackTransactionAsync();
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error updating service");
        }
    }
}