using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models;

namespace Pharmacy.Infrastructure.Data.Configuration;


public class ProductProviderConfigurations : IEntityTypeConfiguration<ProductProvider>
{
    public void Configure(EntityTypeBuilder<ProductProvider> builder)
    {
        builder.HasKey(provider => provider.Id);
    }
}
