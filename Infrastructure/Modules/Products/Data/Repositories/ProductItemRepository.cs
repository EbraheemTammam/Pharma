using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Generics.Repositories;


public class ProductItemRepository : GenericRepository<ProductItem>
{
    public ProductItemRepository(ApplicationDbContext context) : base(context) {}

    public override IEnumerable<ProductItem> GetAll() => _dbSet.Include(item => item.Product).ToList();
    public override IEnumerable<ProductItem> Filter(Func<ProductItem, bool> filter) =>
        _dbSet.Include(item => item.Product).Where(filter).ToList();
}
