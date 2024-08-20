using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Orders.Models;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;


public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasMany(order => order.Items)
        .WithOne()
        .HasForeignKey(item => item.OrderId);

        builder.HasOne(order => order.CreatedBy)
        .WithMany()
        .HasForeignKey(order => order.UserId);

        builder.HasOne(order => order.Customer)
        .WithMany()
        .HasForeignKey(order => order.CustomerId);
    }
}
