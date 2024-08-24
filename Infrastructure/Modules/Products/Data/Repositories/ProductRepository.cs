using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Infrastructure.Generics;
using Pharmacy.Infrastructure.Generics.Repositories;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Repositories;


public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) {}

    public async Task<Product?> GetByBarcode(string barcode) =>
        await _dbSet.SingleOrDefaultAsync(obj => obj.Barcode == barcode);
}
