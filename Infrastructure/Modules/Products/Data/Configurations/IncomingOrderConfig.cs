using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;


public class OrderItemConfigs : IEntityTypeConfiguration<IncomingOrder>
{
    public void Configure(EntityTypeBuilder<IncomingOrder> builder)
    {
        builder.HasKey(order => order.Id);

        builder.HasOne(order => order.Provider)
        .WithMany()
        .HasForeignKey(order => order.ProviderId);

        builder.HasMany(order => order.Products)
        .WithOne(product => product.IncomingOrder)
        .HasForeignKey(product => product.IncomingOrderId);
    }
}
