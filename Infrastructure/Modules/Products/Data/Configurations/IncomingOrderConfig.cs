using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;


public class OrderItemConfigs : IEntityTypeConfiguration<IncomingOrder>
{
    public void Configure(EntityTypeBuilder<IncomingOrder> builder)
    {
        builder.ToTable("finance_incomingorder")
        .HasKey(order => order.Id);

        builder.Property(product => product.Id)
        .HasColumnName("id");

        builder.Property(product => product.Price)
        .HasColumnName("price");

        builder.Property(product => product.Paid)
        .HasColumnName("paid");

        builder.Property(product => product.CreatedAt)
        .HasColumnName("time");

        builder.Property(product => product.ProviderId)
        .HasColumnName("company_id");

        builder.HasOne(order => order.Provider)
        .WithMany()
        .HasForeignKey(order => order.ProviderId);

        builder.HasMany(order => order.Products)
        .WithOne(product => product.IncomingOrder)
        .HasForeignKey(product => product.IncomingOrderId);
    }
}
