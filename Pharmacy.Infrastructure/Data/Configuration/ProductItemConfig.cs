using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models;

namespace Pharmacy.Infrastructure.Data.Configuration;

public class ProductItemConfigurations : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.HasKey(item => item.Id);

        builder.HasOne(item => item.Product)
        .WithMany()
        .HasForeignKey(item => item.ProductId);
    }
}
