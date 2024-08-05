using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models.ProductsModule;

namespace Pharmacy.Infrastructure.Data.Configurations.ProductModule;


public class ScarceProductConfigurations : IEntityTypeConfiguration<ScarceProduct>
{
    public void Configure(EntityTypeBuilder<ScarceProduct> builder)
    {
        builder.ToTable("products_scarce")
        .HasKey(scarce => scarce.Id);

        builder.Property(scarce => scarce.Id)
        .HasColumnName("id")
        .IsRequired()
        .ValueGeneratedOnAdd();

        builder.Property(scarce => scarce.Name)
        .HasColumnName("name")
        .HasMaxLength(250)
        .IsRequired();

        builder.Property(scarce => scarce.Amount)
        .HasColumnName("amount")
        .IsRequired();
    }
}
