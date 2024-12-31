using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IProductProviderService
{
    Task<Result<IEnumerable<ProductProviderDTO>>> GetAll();
    Task<Result<ProductProviderDTO>> GetById(Guid id);
    Task<Result<ProductProviderDTO>> Create(ProductProviderCreateDTO schema);
    Task<Result<ProductProviderDTO>> Update(Guid id, ProductProviderCreateDTO schema);
    Task<Result> Delete(Guid id);
}
