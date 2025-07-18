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
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.cart_id)
                .IsRequired();

            builder.Property(o => o.payment_id)
                .IsRequired();

            // Order - Cart relationship (Many-to-One)
            builder.HasOne(o => o.Cart)
                .WithMany()
                .HasForeignKey(o => o.cart_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Order - PaymentMethod relationship (Many-to-One)
            builder.HasOne(o => o.PaymentMethod)
                .WithMany()
                .HasForeignKey(o => o.payment_id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Order
                {
                    Id = 1,
                    cart_id = 1,
                    payment_id = 1
                },
                new Order
                {
                    Id = 2,
                    cart_id = 2,
                    payment_id = 2
                }
            );
        }
    }
}