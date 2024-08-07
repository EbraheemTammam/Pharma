using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services;


public interface IProductService
{
    BaseResponse GetAll();
    BaseResponse GetById(int id);
    BaseResponse Create(ProductCreateDTO schema);
    BaseResponse Update(int id, ProductCreateDTO schema);
    BaseResponse Remove(int id);
}
