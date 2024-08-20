using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;

public class ProductItemConfigurations : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.HasOne(item => item.Product)
        .WithMany()
        .HasForeignKey(item => item.ProductId);
    }
}
