using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;


public class CustomUserConfigs : IEntityTypeConfiguration<IncomingOrder>
{
    public void Configure(EntityTypeBuilder<IncomingOrder> builder)
    {
        builder.HasKey(user => user.Id);
    }
}
