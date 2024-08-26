using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Service.Interfaces;


public interface IProductService : IService<Guid>
{
    Task<BaseResponse> GetByBarcode(string barcode);
    Task<BaseResponse> Create(ProductCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ProductCreateDTO schema);
}
