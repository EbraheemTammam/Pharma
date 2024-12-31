using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IScarceProductService
{
    Task<Result<IEnumerable<ScarceProductDTO>>> GetAll();
    Task<Result<ScarceProductDTO>> GetById(Guid id);
    Task<Result<ScarceProductDTO>> Create(ScarceProductCreateDTO schema);
    Task<Result<ScarceProductDTO>> Update(Guid id, ScarceProductCreateDTO schema);
    Task<Result> Delete(Guid id);
}
