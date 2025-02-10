using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Entities;

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
            await _serviceRepository.BeginTransactionAsync();
            if (unitEntity == null)
            {
                unitEntity = new UnitTypeEntity { UnitType = form.UnitType };
                await _unitTypeRepository.AddAsync(unitEntity);
                var unitSaveResult = await _unitTypeRepository.SaveAsync();
                if (unitSaveResult == false)
                    throw new Exception("Error saving unit");
            }

            var serviceEntity = ServiceFactory.CreateEntity(form, unitEntity.Id);
            await _serviceRepository.AddAsync(serviceEntity);
            var saveResult = await _serviceRepository.SaveAsync();
            if (saveResult == false)
                throw new Exception("Error saving service");

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

            await _serviceRepository.BeginTransactionAsync();
            entityToUpdate = ServiceFactory.CreateEntity(updateForm, entityToUpdate.UnitId);
            var result = await _serviceRepository.UpdateAsync(x => x.Id == id, entityToUpdate);
            if (result)
                await _serviceRepository.SaveAsync();
            else
                throw new Exception("Error saving service");

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

    public async Task<IResponseResult> DeleteServiceAsync(int id)
    {
        try
        {
            var entity = await _serviceRepository.GetAsync(x => x.Id == id);
            if (entity == null)
                return ResponseResult.NotFound("Service not found");

            await _serviceRepository.BeginTransactionAsync();
            bool result = await _serviceRepository.DeleteAsync(x => x.Id == id);
            if (result)
                await _serviceRepository.SaveAsync();
            else
                throw new Exception("Error deleting service");

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
}