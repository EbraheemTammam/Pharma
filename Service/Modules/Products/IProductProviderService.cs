using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services;


public interface IProductProviderService
{
    BaseResponse GetAll();
    BaseResponse GetById(Guid id);
    BaseResponse Create(ProductProviderCreateDTO schema);
    BaseResponse Update(Guid id, ProductProviderCreateDTO schema);
    BaseResponse Remove(Guid id);
}
