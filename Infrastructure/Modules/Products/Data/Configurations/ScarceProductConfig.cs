using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;


public class ScarceProductConfigurations : IEntityTypeConfiguration<ScarceProduct>
{
    public void Configure(EntityTypeBuilder<ScarceProduct> builder)
    {
        builder.ToTable("products_scarce")
        .HasKey(scarce => scarce.Id);

        builder.Property(scarce => scarce.Id)
        .HasColumnName("id");

        builder.Property(scarce => scarce.Name)
        .HasColumnName("name");

        builder.Property(scarce => scarce.Amount)
        .HasColumnName("amount");
    }
}
