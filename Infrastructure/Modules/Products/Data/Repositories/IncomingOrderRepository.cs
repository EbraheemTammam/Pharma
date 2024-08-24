using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Infrastructure.Generics.Repositories;
using Pharmacy.Infrastructure.Generics;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Repositories;


public class IncomingOrderRepository : GenericRepository<IncomingOrder>
{
    public IncomingOrderRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<IEnumerable<IncomingOrder>> GetAll() =>
        await _dbSet.Include(order => order.Provider).ToListAsync();
}
