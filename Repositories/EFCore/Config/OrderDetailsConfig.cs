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
    public class OrderDetailsConfig : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.HasKey(od => od.orderDetails_id);

            builder.Property(od => od.order_id)
                .IsRequired();

            builder.Property(od => od.totalAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(od => od.isPaid)
                .IsRequired();

            builder.Property(od => od.status)
                .HasMaxLength(50);

            // OrderDetails - Order relationship (Many-to-One)
            builder.HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(od => od.order_id)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderDetails - Product relationship (Many-to-One)
            builder.HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey("product_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new OrderDetails
                {
                    orderDetails_id = 1,
                    order_id = 1,
                    totalAmount = 100.00m,
                    isPaid = true,
                    status = "Completed"
                },
                new OrderDetails
                {
                    orderDetails_id = 2,
                    order_id = 2,
                    totalAmount = 200.00m,
                    isPaid = false,
                    status = "Pending"
                }
            );
        }
    }
}