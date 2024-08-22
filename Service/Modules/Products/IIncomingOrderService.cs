using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services.Modules.Products;


public interface IIncomingOrderService
{
    BaseResponse GetAll();
    BaseResponse GetById(Guid id);
    BaseResponse GetItems(Guid id);
    BaseResponse Create(IncomingOrderCreateDTO schema);
    BaseResponse Update(Guid id, IncomingOrderUpdateDTO schema);
    BaseResponse Delete(Guid id);
}
