﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToysAndGamesDataAccess.Data;

#nullable disable

namespace ToysAndGames.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220723083254_createProductsCatalog")]
    partial class createProductsCatalog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ToysAndGames_Model.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AgeRestriction")
                        .HasColumnType("int");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasPrecision(6, 2)
                        .HasColumnType("decimal(6,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasCheckConstraint("CK_Products_AgeRestriction_Range", "(AgeRestriction >= 0 AND AgeRestriction <= 100)");

                    b.HasCheckConstraint("CK_Products_Price_Range", "(Price >= 1 AND Price <= 1000)");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgeRestriction = 3,
                            Company = "HotWheels",
                            Description = "Diecast cars",
                            Name = "HotWheels Car",
                            Price = 25.00m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
