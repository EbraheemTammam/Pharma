using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services;


public interface IProductService
{
    BaseResponse GetAll();
    BaseResponse GetById(Guid id);
    BaseResponse GetByBarcode(string barcode);
    BaseResponse Create(ProductCreateDTO schema);
    BaseResponse Update(Guid id, ProductCreateDTO schema);
    BaseResponse Delete(Guid id);
}
