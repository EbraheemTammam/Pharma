using Pharmacy.Services;
using Pharmacy.Domain.Models.ProductsModule;
using Pharmacy.Shared.DTOs.ProductsModule;
using Pharmacy.Application.Utilities;
using Pharmacy.Domain.Interfaces;
namespace Pharmacy.Application.Services.ProductsModule;



public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;

    public ProductService(IRepositoryManager repoManager)
    {
        _repositoryManager = repoManager;
    }

    public ProductDTO Create(ProductCreateDTO schema)
    {
        Product product = _repositoryManager.Products.Add(schema.ToModel());
        _repositoryManager.Products.Save();
        return product.ToDTO();
    }

    public IEnumerable<ProductDTO> GetAll() =>
        _repositoryManager.Products.GetAll().ConvertAll(obj => obj.ToDTO());

    public ProductDTO GetById(int id) =>
        _repositoryManager.Products.GetById(id).ToDTO();

    public ProductDTO Update(ProductCreateDTO schema)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
}
