using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubcategoryConfig : IEntityTypeConfiguration<Subcategory>
{
    public void Configure(EntityTypeBuilder<Subcategory> builder)
    {
        builder.HasKey(sc => sc.SubcategoryId);

        builder.Property(sc => sc.SubcategoryId)
            .IsRequired();

        builder.Property(sc => sc.SubcategoryName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(sc => sc.Category)
            .WithMany(c => c.Subcategories)
            .HasForeignKey(sc => sc.category_id);

        builder.HasData(
            new Subcategory
            {
                SubcategoryId = 1,
                SubcategoryName = "Smartphones",
                category_id = 1
            },
            new Subcategory
            {
                SubcategoryId = 2,
                SubcategoryName = "Laptops",
                category_id = 1
            },
            new Subcategory
            {
                SubcategoryId = 3,
                SubcategoryName = "Tablets",
                category_id = 1
            }
        );
    }
}
