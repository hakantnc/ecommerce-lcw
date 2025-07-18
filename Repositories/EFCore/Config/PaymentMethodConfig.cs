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
    public class PaymentMethodConfig : IEntityTypeConfiguration<Entities.Models.PaymentMethods>
    {
        public void Configure(EntityTypeBuilder<PaymentMethods> builder)
        {
            builder.HasKey(pm => pm.Id);
            builder.Property(pm => pm.method_type)
                .IsRequired()
                .HasMaxLength(70);
            builder.Property(pm => pm.cardHolder)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(pm => pm.cardNumber)
                .IsRequired()
                .HasMaxLength(16);
            builder.Property(pm => pm.CVV)
                .IsRequired()
                .HasMaxLength(3);
            builder.Property(pm => pm.ExpiryDate)
                .IsRequired();
            builder.Property(pm => pm.BillingAddress)
                .IsRequired()
                .HasMaxLength(200);
            builder.HasOne(pm => pm.Customer)
                .WithMany(c => c.PaymentMethods)
                .HasForeignKey(pm => pm.customer_id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new PaymentMethods
                {
                    Id = 1,
                    method_type = "Credit Card",
                    cardHolder = "John Doe",
                    cardNumber = "1234567890123456",
                    CVV = "123",
                    ExpiryDate = new DateTime(2024, 01, 01),
                    BillingAddress = "123 Main St, Anytown, USA",
                    customer_id = 1
                },
                new PaymentMethods
                {
                    Id = 2,
                    method_type = "Debit Card",
                    cardHolder = "Jane Smith",
                    cardNumber = "6543210987654321",
                    CVV = "456",
                    ExpiryDate = new DateTime(2024, 01, 01),
                    BillingAddress = "456 Elm St, Othertown, USA",
                    customer_id = 2
                }
            );
        }
    }
}
