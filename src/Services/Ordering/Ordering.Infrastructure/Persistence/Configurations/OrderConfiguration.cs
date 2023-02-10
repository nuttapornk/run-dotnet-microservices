using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.Property(e => e.Username).IsRequired().HasMaxLength(100);
        builder.Property(e => e.FirstName).HasMaxLength(100);
        builder.Property(e => e.LastName).HasMaxLength(100);
        builder.Property(e => e.ZipCode).HasMaxLength(5);
        builder.Property(e => e.EmailAddress).IsRequired().HasMaxLength(100);
        builder.Property(e => e.TotalPrice).IsRequired().HasColumnType("decimal(8,2)");
    }
}
