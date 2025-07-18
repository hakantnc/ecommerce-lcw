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
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            
            builder.HasKey(c => c.category_id);
            builder.Property(c=> c.category_name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.category_description)
                .HasMaxLength(500);

            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.Restrict) // Prevent category deletion if it has products
                .HasForeignKey(p => p.category_id);

            builder.HasData(
                new Category
                {
                    category_id = 1,
                    category_name = "Electronics",
                    category_description = "Devices and gadgets"
                },
                new Category
                {
                    category_id = 2,
                    category_name = "Components",
                    category_description = "Electronic components and parts"
                },
                new Category
                {
                    category_id = 3,
                    category_name = "Accessories",
                    category_description = "Various electronic accessories"
                }
                );
                }
    }
}
