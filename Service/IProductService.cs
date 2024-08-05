using Pharmacy.Shared.DTOs.ProductsModule;

namespace Pharmacy.Services;


public interface IProductService
{
    IEnumerable<ProductDTO> GetAll();
    ProductDTO GetById(int id);
    ProductDTO Create(ProductCreateDTO schema);
    ProductDTO Update(ProductCreateDTO schema);
    void Remove(int id);
}
