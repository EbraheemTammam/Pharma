using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Infrastructure.Generics;
using Pharmacy.Infrastructure.Generics.Repositories;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Repositories;


public class ProductItemRepository : GenericRepository<ProductItem>
{
    public ProductItemRepository(ApplicationDbContext context) : base(context) {}

    public override IEnumerable<ProductItem> GetAll() => _dbSet.Include(item => item.Product).ToList();
    public override IEnumerable<ProductItem> Filter(Func<ProductItem, bool> filter) =>
        _dbSet.Include(item => item.Product).Where(filter).ToList();
}
