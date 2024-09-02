using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Repositories;


public class IncomingOrderRepository : GenericRepository<IncomingOrder>
{
    public IncomingOrderRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<IEnumerable<IncomingOrder>> GetAll() =>
        await _dbSet.Include(order => order.Provider).ToListAsync();
}
