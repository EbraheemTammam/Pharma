using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models.ProductsModule;

namespace Pharmacy.Infrastructure.Data.Configurations.ProductModule;


public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products_type")
        .HasKey(product => product.Id);

        builder.Property(product => product.Id)
        .HasColumnName("id")
        .IsRequired()
        .ValueGeneratedOnAdd();

        builder.Property(product => product.Name)
        .HasColumnName("name")
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(product => product.NumberOfElements)
        .HasColumnName("number_of_elements")
        .IsRequired();

        builder.Property(product => product.PricePerElement)
        .HasColumnName("price_per_element")
        .IsRequired()
        .HasPrecision(2);

        builder.Property(product => product.Barcode)
        .HasColumnName("barcode")
        .HasMaxLength(15);

        builder.Property(product => product.OwnedElements)
        .HasColumnName("owned_elements")
        .HasDefaultValue(0);

        builder.Property(product => product.IsLack)
        .HasColumnName("lack")
        .HasDefaultValue(false);

        builder.Property(product => product.Minimum)
        .HasColumnName("minimum")
        .HasDefaultValue(0);
    }
}
