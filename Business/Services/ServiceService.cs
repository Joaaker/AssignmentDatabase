using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<IResponseResult> CreateServiceAsync(ServiceRegistrationForm form)
    {
        if (form == null)
            return ResponseResult.BadRequest("Invalid form");

        try
        {


        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error retrieving service");
        }


    }

    public Task<IResponseResult> DeleteServiceAsync(int id)
    {
        try
        {


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


        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error geting services");
        }
    }

    public async Task<IResponseResult> GetServiceByIdAsync(int id)
    {
        try
        {


        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error geting service");
        }
    }

    public async Task<IResponseResult> UpdateServiceAsync(int id, ServiceRegistrationForm updateForm)
    {
        try
        {


        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult.Error("Error updating service");
        }
    }
}
