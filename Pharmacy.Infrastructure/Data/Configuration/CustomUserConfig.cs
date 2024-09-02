using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models;

namespace Pharmacy.Infrastructure.Data.Configuration;


public class CustomUserConfigs : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
    }
}
