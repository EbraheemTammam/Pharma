using Pharmacy.Services.Interfaces;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Service.Services;


public interface IProductProviderService : IService<Guid>
{
    Task<BaseResponse> Create(ProductProviderCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ProductProviderCreateDTO schema);
}
