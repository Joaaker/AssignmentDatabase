using Business.Dtos;
using Business.Interfaces;
using Data.Interfaces;

namespace Business.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public Task<IResponseResult> CreateServiceAsync(ServiceRegistrationForm form)
    {
        throw new NotImplementedException();
    }

    public Task<IResponseResult> DeleteServiceAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IResponseResult> GetAllServicesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IResponseResult> GetServiceByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IResponseResult> UpdateServiceAsync(int id, ServiceRegistrationForm updateForm)
    {
        throw new NotImplementedException();
    }
}
