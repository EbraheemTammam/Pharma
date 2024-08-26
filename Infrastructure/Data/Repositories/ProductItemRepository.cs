using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Models;

namespace Pharmacy.Infrastructure.Data.Repositories;


public class ProductItemRepository : GenericRepository<ProductItem>
{
    public ProductItemRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<IEnumerable<ProductItem>> GetAll() =>
        await _dbSet.Include(item => item.Product).ToListAsync();
    public override async Task<IEnumerable<ProductItem>> Filter(Expression<Func<ProductItem, bool>> filter) =>
        await _dbSet.Include(item => item.Product).Where(filter).ToListAsync();
}
