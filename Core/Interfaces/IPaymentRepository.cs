using Pharmacy.Domain.Models;

namespace Pharmacy.Domain.Interfaces;


public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment?> GetByIdAndCustomer(int id, Guid customerId);
}
