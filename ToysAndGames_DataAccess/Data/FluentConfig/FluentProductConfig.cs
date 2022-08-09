﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToysAndGamesModel.Models;

namespace ToysAndGamesDataAccess.Data.FluentConfig
{
    public class FluentProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder.HasKey(p => p.Id);
            modelBuilder.HasCheckConstraint("CK_Products_AgeRestriction_Range", "(AgeRestriction >= 0 AND AgeRestriction <= 100)");
            modelBuilder.HasCheckConstraint("CK_Products_Price_Range", "(Price >= 1 AND Price <= 1000)");
            modelBuilder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Property(p => p.Description).HasMaxLength(100);
            modelBuilder.Property(p => p.Company).IsRequired().HasMaxLength(50);
            modelBuilder.Property(p => p.Price).IsRequired().HasColumnType("decimal").HasPrecision(6,2);
            modelBuilder.HasData(new Product
            {
                Id = 1,
                Name = "HotWheels Car",
                Description = "Diecast cars",
                AgeRestriction = 3,
                Company = "HotWheels",
                Price = 25.00M,
                ImagePath = "",
                ImageBytes = new byte[] { 0x0 }
            });
        }
    }
}
