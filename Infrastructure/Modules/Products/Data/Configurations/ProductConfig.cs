using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;


public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products_type")
        .HasKey(product => product.Id);

        builder.Property(product => product.Id)
        .HasColumnName("id");

        builder.Property(product => product.Name)
        .HasColumnName("name");

        builder.Property(product => product.NumberOfElements)
        .HasColumnName("number_of_elements");

        builder.Property(product => product.PricePerElement)
        .HasColumnName("price_per_element");

        builder.Property(product => product.Barcode)
        .HasColumnName("barcode");

        builder.Property(product => product.OwnedElements)
        .HasColumnName("owned_elements");

        builder.Property(product => product.IsLack)
        .HasColumnName("lack");

        builder.Property(product => product.Minimum)
        .HasColumnName("minimum");
    }
}
