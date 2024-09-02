using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IProductProviderService : IService<Guid>
{
    Task<BaseResponse> Create(ProductProviderCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ProductProviderCreateDTO schema);
}
