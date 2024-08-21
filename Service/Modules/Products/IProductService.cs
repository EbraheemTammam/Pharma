using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services;


public interface IProductService
{
    BaseResponse GetAll();
    BaseResponse GetById(Guid id, bool AsDTO = true);
    BaseResponse GetByBarcode(string barcode, bool AsDTO = true);
    BaseResponse Create(ProductCreateDTO schema);
    BaseResponse Update(Guid id, ProductCreateDTO schema);
    BaseResponse Delete(Guid id);
}
