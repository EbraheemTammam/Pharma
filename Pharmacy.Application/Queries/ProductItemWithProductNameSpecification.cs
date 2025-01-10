using System.Linq.Expressions;
using Pharmacy.Application.DTOs;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Queries;

public class ProductItemWithProductNameSpecification : Specification<ProductItem, ProductItemDTO>
{
    public ProductItemWithProductNameSpecification(Expression<Func<ProductItem, bool>> criteria) : base(criteria)
    {
        Selector = item => new ProductItemDTO
        {
            Id = item.Id,
            NumberOfBoxes = item.NumberOfBoxes,
            ProductName = item.Product!.Name,
            Price = (decimal)(item.NumberOfBoxes * item.NumberOfElements * item.Product.PricePerElement)
        };
    }

    public ProductItemWithProductNameSpecification(Guid orderId) : base(item => item.IncomingOrderId == orderId)
    {
        Selector = item => new ProductItemDTO
        {
            Id = item.Id,
            NumberOfBoxes = item.NumberOfBoxes,
            ProductName = item.Product!.Name,
            Price = (decimal)(item.NumberOfBoxes * item.Product.NumberOfElements * item.Product.PricePerElement)
        };
    }
}
