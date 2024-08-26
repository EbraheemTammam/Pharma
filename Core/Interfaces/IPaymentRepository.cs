using Pharmacy.Domain.Generics;
using Pharmacy.Domain.Modules.Orders.Models;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Domain.Interfaces;


public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment?> GetByIdAndCustomer(int id, Guid customerId);
}
