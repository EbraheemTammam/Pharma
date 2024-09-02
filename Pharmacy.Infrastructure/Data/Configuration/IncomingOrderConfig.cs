using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models;

namespace Pharmacy.Infrastructure.Data.Configuration;


public class IncomingOrderConfigurations : IEntityTypeConfiguration<IncomingOrder>
{
    public void Configure(EntityTypeBuilder<IncomingOrder> builder)
    {
        builder.HasKey(order => order.Id);

        builder.HasOne(order => order.Provider)
        .WithMany()
        .HasForeignKey(order => order.ProviderId);

        builder.HasMany(order => order.Items)
        .WithOne()
        .HasForeignKey(item => item.IncomingOrderId);
    }
}
