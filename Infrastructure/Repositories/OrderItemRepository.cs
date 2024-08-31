using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Models;

namespace Pharmacy.Infrastructure.Data.Repositories;


public class OrderItemRepository : GenericRepository<OrderItem>
{
    public OrderItemRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<IEnumerable<OrderItem>> GetAll() =>
        await _dbSet.Include(item => item.Product).ToListAsync();
    public override async Task<IEnumerable<OrderItem>> Filter(Expression<Func<OrderItem, bool>> filter) =>
        await _dbSet.Include(item => item.Product).Where(filter).ToListAsync();
}
