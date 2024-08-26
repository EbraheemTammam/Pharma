using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models;

namespace Pharmacy.Infrastructure.Data.Configuration;


public class ScarceProductConfigurations : IEntityTypeConfiguration<ScarceProduct>
{
    public void Configure(EntityTypeBuilder<ScarceProduct> builder)
    {
        builder.HasKey(product => product.Id);
    }
}
