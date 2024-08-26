using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Service.Interfaces;


public interface IProductProviderService : IService<Guid>
{
    Task<BaseResponse> Create(ProductProviderCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ProductProviderCreateDTO schema);
}
