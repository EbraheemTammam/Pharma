using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Generics;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Generics.Repositories;


public class IncomingOrderRepository : GenericRepository<IncomingOrder>
{
    public IncomingOrderRepository(ApplicationDbContext context) : base(context) {}

    public override IEnumerable<IncomingOrder> GetAll() => _dbSet.Include(order => order.Provider).ToList();
}
