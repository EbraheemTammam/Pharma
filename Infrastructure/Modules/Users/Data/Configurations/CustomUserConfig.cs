using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Modules.Users.Models;

namespace Pharmacy.Infrastructure.Modules.Products.Data.Configurations;


public class CustomUserConfigs : IEntityTypeConfiguration<CustomUser>
{
    public void Configure(EntityTypeBuilder<CustomUser> builder)
    {
        builder.HasKey(user => user.Id);
    }
}
