using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Repositories;


public class ProductItemRepository : GenericRepository<ProductItem>, IProductItemRepository
{
    public ProductItemRepository(ApplicationDbContext context) : base(context) {}

    public override async Task<IEnumerable<ProductItem>> GetAll() =>
        await _dbSet.Include(item => item.Product).ToListAsync();
    public override async Task<IEnumerable<ProductItem>> Filter(Expression<Func<ProductItem, bool>> filter) =>
        await _dbSet.Include(item => item.Product).Where(filter).ToListAsync();

    public async Task<ProductItem?> GetLastByProduct(Guid productId) =>
        await _dbSet.Where(item => item.ProductId == productId).LastAsync();
}
