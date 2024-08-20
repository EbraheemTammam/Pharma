using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;


public class ProductProviderConfigurations : IEntityTypeConfiguration<ProductProvider>
{
    public void Configure(EntityTypeBuilder<ProductProvider> builder)
    {
        builder.HasKey(provider => provider.Id);
    }
}
