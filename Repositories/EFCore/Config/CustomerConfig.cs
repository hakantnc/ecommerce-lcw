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
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Password)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(c => c.FirstName)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(c => c.LastName)
               .HasMaxLength(20)
               .IsRequired();
            builder.Property(c => c.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20);
            builder.Property(c => c.Address)
                .HasMaxLength(200);
            builder.Property(c => c.City)
                .HasMaxLength(100);
            builder.Property(c => c.Country)
                .HasMaxLength(100);

            builder.HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "Burak",
                    LastName = "İbicioglu",
                    Password = "kankazenginsin",
                    PhoneNumber = "0532 123 45 67",
                    Email = "kusyuvasi@gmail.com",
                    Address = "Kuş Yuvası Mah. No: 123",
                    City = "İstanbul",
                    Country = "Türkiye"
                  
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Semir",
                    LastName = "Kagan",
                    Password = "abinedesemki123",
                    PhoneNumber = "0532 123 45 67",
                    Email = "yaraliayak@gmail.com",
                    Address = "Kotor Mah. No: 123",
                    City = "Dobrota",
                    Country = "Montenegro"

                }







                );
        }
    }
}
