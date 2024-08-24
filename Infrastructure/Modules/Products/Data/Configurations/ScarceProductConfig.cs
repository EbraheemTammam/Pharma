using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;


public class ScarceProductConfigurations : IEntityTypeConfiguration<ScarceProduct>
{
    public void Configure(EntityTypeBuilder<ScarceProduct> builder)
    {
        builder.HasKey(product => product.Id);
    }
}
