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
    public class CartItemConfig : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.cartItem_id);
            
            builder.Property(ci => ci.cart_id)
                .IsRequired();
            
            builder.Property(ci => ci.product_id)
                .IsRequired();
            
            builder.Property(ci => ci.quantity)
                .IsRequired();

            // Ignore computed property
            builder.Ignore(ci => ci.Price);

            // CartItem - Cart relationship (Many-to-One)
            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.cart_id)
                .OnDelete(DeleteBehavior.Cascade);

            // CartItem - Product relationship (Many-to-One)
            builder.HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.product_id)
                .OnDelete(DeleteBehavior.Restrict); // Prevent product deletion if it's in cart

            builder.HasData(
                new CartItem
                {
                    cartItem_id = 1,
                    cart_id = 1,
                    product_id = 1,
                    quantity = 2,
                    addedDate = new DateTime(2025, 1, 1, 10, 0, 0)
                },
                new CartItem
                {
                    cartItem_id = 2,
                    cart_id = 1,
                    product_id = 2,
                    quantity = 1,
                    addedDate = new DateTime(2025, 1, 1, 11, 0, 0)
                },
                new CartItem
                {
                    cartItem_id = 3,
                    cart_id = 2,
                    product_id = 3,
                    quantity = 3,
                    addedDate = new DateTime(2025, 1, 1, 12, 0, 0)
                }
            );
        }
    }
}