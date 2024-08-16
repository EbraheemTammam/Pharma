using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services;


public interface IIncomingOrderService
{
    BaseResponse GetAll();
    BaseResponse GetById(Guid id);
    BaseResponse Create(IncomingOrderCreateDTO schema);
    BaseResponse Update(Guid id, IncomingOrderUpdateDTO schema);
    BaseResponse Delete(Guid id);
}
