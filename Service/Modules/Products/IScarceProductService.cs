using Pharmacy.Services.Interfaces;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services.Modules.Products;


public interface IScarceProductService : IService<Guid>
{
    Task<BaseResponse> Create(ScarceProductCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ScarceProductCreateDTO schema);
}
