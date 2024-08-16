using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Generics;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Generics.Repositories;


public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) {}

    public Product? GetByBarcode(string barcode) =>
        _dbSet.SingleOrDefault(obj => obj.Barcode == barcode);
}
