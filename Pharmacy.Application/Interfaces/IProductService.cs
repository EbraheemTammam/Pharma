using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IProductService
{
    Task<Result<IEnumerable<ProductDTO>>> GetAll();
    Task<Result<IEnumerable<ProductDTO>>> GetLacked();
    Task<Result<IEnumerable<ProductItemDTO>>> GetAboutToExpire();
    Task<Result<ProductDTO>> GetById(Guid id);
    Task<Result<ProductDTO>> Create(ProductCreateDTO schema);
    Task<Result<ProductDTO>> Update(Guid id, ProductCreateDTO schema);
    Task<Result> Delete(Guid id);
}
