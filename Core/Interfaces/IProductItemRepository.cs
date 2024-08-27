using Pharmacy.Domain.Models;

namespace Pharmacy.Domain.Interfaces;


public interface IProductItemRepository : IRepository<ProductItem>
{
    Task<ProductItem?> GetLastByProduct(Guid productId);
}
