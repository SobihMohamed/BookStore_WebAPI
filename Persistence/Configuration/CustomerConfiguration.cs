using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasOne(c => c.ApplicationUser)
                   .WithOne(u => u.Customer)
                   .HasForeignKey<Customer>(c => c.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}