using Pharmacy.Services.Interfaces;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services.Modules.Products;


public interface IProductProviderService : IService<Guid>
{
    Task<BaseResponse> Create(ProductProviderCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ProductProviderCreateDTO schema);
}
