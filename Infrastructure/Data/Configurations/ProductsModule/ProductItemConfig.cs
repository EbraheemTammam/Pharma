using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models.ProductsModule;

namespace Pharmacy.Infrastructure.Data.Configurations.ProductModule;


public class ProductItemConfigurations : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.ToTable("products_product")
        .HasKey(customer => customer.Id);

        builder.Property(customer => customer.Id)
        .HasColumnName("id")
        .IsRequired()
        .ValueGeneratedOnAdd();

        builder.Property(customer => customer.ExpirationDate)
        .HasColumnName("expiration")
        .IsRequired();

        builder.Property(customer => customer.NumberOfElements)
        .HasColumnName("number_of_elements")
        .IsRequired();

        builder.Property(customer => customer.NumberOfBoxes)
        .HasColumnName("number_of_boxes")
        .IsRequired();

        builder.Property(customer => customer.ProductId)
        .HasColumnName("type_id")
        .IsRequired();

        builder.Property(customer => customer.IncomingOrderId)
        .HasColumnName("incoming_order_id")
        .IsRequired();

        builder.HasOne(item => item.Product)
        .WithMany()
        .HasForeignKey(item => item.ProductId);
    }
}
