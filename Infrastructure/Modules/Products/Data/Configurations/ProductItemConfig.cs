using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;

public class ProductItemConfigurations : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.ToTable("products_product")
        .HasKey(customer => customer.Id);

        builder.Property(customer => customer.Id)
        .HasColumnName("id");

        builder.Property(customer => customer.ExpirationDate)
        .HasColumnName("expiration");

        builder.Property(customer => customer.NumberOfElements)
        .HasColumnName("number_of_elements");

        builder.Property(customer => customer.NumberOfBoxes)
        .HasColumnName("number_of_boxes");

        builder.Property(customer => customer.ProductId)
        .HasColumnName("type_id");

        builder.Property(customer => customer.IncomingOrderId)
        .HasColumnName("incoming_order_id");

        builder.HasOne(item => item.Product)
        .WithMany()
        .HasForeignKey(item => item.ProductId);
    }
}
