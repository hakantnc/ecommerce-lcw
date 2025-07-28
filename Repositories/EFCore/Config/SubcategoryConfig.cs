using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubcategoryConfig : IEntityTypeConfiguration<Subcategory>
{
    public void Configure(EntityTypeBuilder<Subcategory> builder)
    {
        builder.HasKey(sc => sc.sub_id);

        builder.Property(sc => sc.sub_id)
            .IsRequired();

        builder.Property(sc => sc.sub_name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(sc => sc.Category)
            .WithMany(c => c.Subcategories)
            .HasForeignKey(sc => sc.category_id);

        builder.HasData(
            new Subcategory
            {
                sub_id = 1,
                sub_name = "Smartphones",
                category_id = 1
            },
            new Subcategory
            {
                sub_id = 2,
                sub_name = "Laptops",
                category_id = 1
            },
            new Subcategory
            {
                sub_id = 3,
                sub_name = "Tablets",
                category_id = 1
            }
        );
    }
}
