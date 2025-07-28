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
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {


            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Description);
            builder.Property(p => p.ImageUrl).HasMaxLength(500);
            builder.Property(p => p.Price)
                .HasPrecision(18, 2);
            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.products)
                .HasForeignKey(p => p.supplier_id);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.category_id)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(p=>p.supplier_id)
                .IsRequired();
            builder.Property(p=>p.category_id)
                .IsRequired();

            builder.HasOne(p => p.Subcategory)
                 .WithMany(sc => sc.Products)
                .HasForeignKey(p => p.sub_id)
                 .OnDelete(DeleteBehavior.SetNull);


            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Price = 100.00m,
                    Stock = 10,
                    ImageUrl = "https://example.com/product1.jpg",
                    IsActive = true,
                    supplier_id = 1,
                    supplier_name = "ABC Electronics",
                    category_id = 1,
                    sub_id = 1

                },
                 new Product
                 {
                     Id = 2,
                     Name = "Product 2",
                     Description = "Description for Product 2",
                     Price = 200.00m,
                     Stock = 20,
                     ImageUrl = "https://example.com/product2.jpg",
                     IsActive = true,
                     supplier_id = 2,
                     supplier_name = "XYZ Components",
                     category_id = 2,
                     sub_id = 2

                 },
                 new Product
                 {
                     Id = 3,
                     Name = "Product 3",
                     Description = "Description for Product 3",
                     Price = 300.00m,
                     Stock = 30,
                     ImageUrl = "https://example.com/product3.jpg",
                     IsActive = true,
                     supplier_id = 2,
                     supplier_name = "XYZ Components",
                     category_id = 1,
                     sub_id = 1

                 }
                 
            );
        }
    }
}
