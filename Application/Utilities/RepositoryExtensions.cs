using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Models;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;

namespace Pharmacy.Application.Utilities;



internal static class RepositoryExtensions
{
    public static async Task<(Product? product, string fieldVal, string fieldType)> GetByIdOrBarcode(this IProductRepository repo, Guid? id, string? barcode)
    {
        if(id is not null) return (await repo.GetById(id), id.ToString(), "id")!;
        return (await repo.GetByBarcode(barcode!), barcode, "barcode")!;
    }

    public static async Task PreDeleteIncomingOrder(this IRepositoryManager manager, Guid id)
    {
        IEnumerable<ProductItem> items = await manager.ProductItems.Filter(obj => obj.IncomingOrderId == id);
        foreach (ProductItem item in items)
        {
            item.Product!.OwnedElements -= item.NumberOfBoxes * item.Product.NumberOfElements;
            manager.Products.Update(item.Product);
            manager.ProductItems.Delete(item);
        }
        await manager.Save();
    }

    public static async Task<BaseResponse> AddAllProductItems(this IRepositoryManager manager, IEnumerable<ProductItemCreateDTO> items, Guid incomingOrderId)
    {
        foreach(ProductItemCreateDTO itemDTO in items)
        {
            //  Check if both Id and Barcode are null
            if(itemDTO.ProductId is null && itemDTO.Barcode is null)
                return new BadRequestResponse("product Id or Barcode must be provided");
            //  Get Product
            var result = await manager.Products.GetByIdOrBarcode(itemDTO.ProductId, itemDTO.Barcode);
            //  Check if null
            Product? product = result.product;
            if(product is null)
                return new NotFoundResponse(result.fieldVal, nameof(Product), result.fieldType);
            //  Add Item and  Update Product
            await manager.ProductItems.Add(itemDTO.ToModel(product, incomingOrderId));
            product.OwnedElements += itemDTO.NumberOfBoxes * product.NumberOfElements;
            manager.Products.Update(product);
        }
        await manager.Save();
        return new OkResponse<bool>(true);
    }
}
