using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models;

namespace Pharmacy.Infrastructure.Data.Configuration;


public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(order => order.Id);

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
