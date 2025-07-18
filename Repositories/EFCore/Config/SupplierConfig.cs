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
    public class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.supplier_id);
            builder.Property(s => s.supplier_id)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.supplier_name)
                .HasMaxLength(100);
            builder.Property(s => s.supplier_address)   
                .HasMaxLength(200)
                .IsRequired();
           builder.Property(s => s.supplier_city)      
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(s => s.supplier_country)
                .HasMaxLength(100);

            builder.Property(s => s.supplier_email)    
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(s => s.supplier_phone)
                .HasMaxLength(50);
            // Configure the relationship with Products if needed
            builder.HasMany(s => s.products)
                   .WithOne(p => p.Supplier)
                   .HasForeignKey(p => p.supplier_id);

            builder.HasData(
           new Supplier
           {
               supplier_id = 1,
               supplier_name = "ABC Electronics",
               supplier_email = "contact@abc-electronics.com",
               supplier_phone = "+1-555-0123",
               supplier_address = "123 Technology Drive",
               supplier_city = "San Francisco",
               supplier_country = "USA"
           },
           new Supplier
           {
               supplier_id = 2,
               supplier_name = "XYZ Components",
               supplier_email = "info@xyz-components.com",
               supplier_phone = "+44-20-7946-0958",
               supplier_address = "456 Innovation Street",
               supplier_city = "London",
               supplier_country = "UK"
           }
       );
        }
    }
}
