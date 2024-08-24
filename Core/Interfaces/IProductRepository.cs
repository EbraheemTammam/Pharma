using Pharmacy.Domain.Generics;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Domain.Interfaces;


public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByBarcode(string barcode);
}
