using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models.ProductsModule;

namespace Pharmacy.Infrastructure.Data.Configurations.ProductModule;


public class ProductProviderConfigurations : IEntityTypeConfiguration<ProductProvider>
{
    public void Configure(EntityTypeBuilder<ProductProvider> builder)
    {
        builder.ToTable("finance_company")
        .HasKey(customer => customer.Id);

        builder.Property(customer => customer.Id)
        .HasColumnName("id")
        .IsRequired()
        .ValueGeneratedOnAdd();

        builder.Property(customer => customer.Name)
        .HasColumnName("name")
        .IsRequired()
        .HasMaxLength(100);
    }
}
