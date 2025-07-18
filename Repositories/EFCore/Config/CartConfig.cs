using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.customer_id)
                .IsRequired();

            builder.Property(c => c.IsActive)
                .IsRequired();

            // Cart - Customer relationship (Many-to-One)
            builder.HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.customer_id)
                .OnDelete(DeleteBehavior.Cascade);

            // Cart - CartItems relationship (One-to-Many)
            builder.HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.cart_id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Cart
                {
                    Id = 1,
                    customer_id = 1,
                    IsActive = true
                },
                new Cart
                {
                    Id = 2,
                    customer_id = 2,
                    IsActive = true
                }
            );
        }
    }
}