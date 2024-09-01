using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Repositories;


public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<IEnumerable<Order>> GetAll() =>
        await _dbSet.Include(order => order.Customer).Include(order => order.CreatedBy).ToListAsync();
    public override async Task<IEnumerable<Order>> Filter(Expression<Func<Order, bool>> filter) =>
        await _dbSet.Include(order => order.Customer).Include(order => order.CreatedBy).Where(filter).ToListAsync();
}
