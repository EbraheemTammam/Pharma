using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Orders.Models;

namespace Pharmacy.Infrastructure.Modules.Orders.Data.Configurations;


public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasMany(customer => customer.Payments)
        .WithOne()
        .HasForeignKey(payment => payment.CustomerId);
    }
}
