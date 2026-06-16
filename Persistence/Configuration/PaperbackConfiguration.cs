using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class PaperbackConfiguration : IEntityTypeConfiguration<Paperback>
    {
        public void Configure(EntityTypeBuilder<Paperback> builder)
        {
            builder.Property(p => p.ShippingWeight)
                   .HasColumnType("decimal(18,2)");
        }
    }
}