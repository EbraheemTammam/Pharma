using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Repositories;


public class PaymentRepositry : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepositry(ApplicationDbContext context) : base(context) {}
    public async Task<Payment?> GetByIdAndCustomer(int id, Guid customerId) =>
        await _dbSet.SingleOrDefaultAsync(obj => obj.Id == id && obj.CustomerId == customerId);
}
