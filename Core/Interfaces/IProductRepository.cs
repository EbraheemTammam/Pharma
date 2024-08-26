using Pharmacy.Domain.Models;

namespace Pharmacy.Domain.Interfaces;


public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByBarcode(string barcode);
}
