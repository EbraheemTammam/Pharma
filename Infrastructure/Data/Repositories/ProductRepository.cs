using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Models;

namespace Pharmacy.Infrastructure.Data.Repositories;


public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) {}

    public async Task<Product?> GetByBarcode(string barcode) =>
        await _dbSet.SingleOrDefaultAsync(obj => obj.Barcode == barcode);
}
