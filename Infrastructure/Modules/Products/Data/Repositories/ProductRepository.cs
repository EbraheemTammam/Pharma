using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Infrastructure.Generics;
using Pharmacy.Infrastructure.Generics.Repositories;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Repositories;


public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) {}

    public Product? GetByBarcode(string barcode) =>
        _dbSet.SingleOrDefault(obj => obj.Barcode == barcode);
}
