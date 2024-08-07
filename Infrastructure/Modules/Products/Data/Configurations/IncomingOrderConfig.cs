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
        .HasColumnName("id")
        .IsRequired()
        .ValueGeneratedOnAdd();

        builder.Property(product => product.Price)
        .HasColumnName("price")
        .IsRequired()
        .HasPrecision(2);

        builder.Property(product => product.Paid)
        .HasColumnName("paid")
        .IsRequired()
        .HasDefaultValue(0);

        builder.Property(product => product.CreatedAt)
        .HasColumnName("time")
        .IsRequired()
        .HasDefaultValue(DateTime.UtcNow);

        builder.Property(product => product.ProviderId)
        .HasColumnName("company_id")
        .IsRequired();

        builder.HasOne(order => order.Provider)
        .WithMany()
        .HasForeignKey(order => order.ProviderId);

        builder.HasMany(order => order.Products)
        .WithOne(product => product.IncomingOrder)
        .HasForeignKey(product => product.IncomingOrderId);
    }
}
