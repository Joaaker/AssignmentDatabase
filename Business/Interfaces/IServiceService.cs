using Business.Dtos;

namespace Business.Interfaces;

public interface IServiceService
{
    Task<IResponseResult> CreateServiceAsync(ServiceRegistrationForm registrationForm);
    Task<IResponseResult> GetAllServicesAsync();
    Task<IResponseResult> GetServiceByIdAsync(int id);
    Task<IResponseResult> UpdateServiceAsync(int id, ServiceRegistrationForm updateForm);
    Task<IResponseResult> DeleteServiceAsync(int id);
}
