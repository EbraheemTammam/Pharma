using Pharmacy.Services.Interfaces;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services.Modules.Products;


public interface IProductService : IService<Guid>
{
    Task<BaseResponse> GetByBarcode(string barcode);
    Task<BaseResponse> Create(ProductCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ProductCreateDTO schema);
}
