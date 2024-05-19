﻿// <auto-generated />
using System;
using FerryBookingClassLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FerryBookingClassLibrary.Migrations
{
    [DbContext(typeof(DbContext))]
    partial class DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FerryBookingClassLibrary.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FerryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FerryId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("FerryBookingClassLibrary.Models.Ferry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MaxCars")
                        .HasColumnType("int");

                    b.Property<int>("MaxGuests")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PricePerCar")
                        .HasColumnType("int");

                    b.Property<int>("PricePerGuest")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ferries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MaxCars = 400,
                            MaxGuests = 980,
                            Name = "MOLSLINJEN (Express 4)",
                            PricePerCar = 249,
                            PricePerGuest = 149
                        },
                        new
                        {
                            Id = 2,
                            MaxCars = 50,
                            MaxGuests = 100,
                            Name = "Standard Ferry",
                            PricePerCar = 197,
                            PricePerGuest = 99
                        });
                });

            modelBuilder.Entity("FerryBookingClassLibrary.Models.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("FerryId")
                        .HasColumnType("int");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("FerryId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("FerryBookingClassLibrary.Models.Car", b =>
                {
                    b.HasOne("FerryBookingClassLibrary.Models.Ferry", "Ferry")
                        .WithMany("Cars")
                        .HasForeignKey("FerryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ferry");
                });

            modelBuilder.Entity("FerryBookingClassLibrary.Models.Guest", b =>
                {
                    b.HasOne("FerryBookingClassLibrary.Models.Car", null)
                        .WithMany("Guests")
                        .HasForeignKey("CarId");

                    b.HasOne("FerryBookingClassLibrary.Models.Ferry", "Ferry")
                        .WithMany("Guests")
                        .HasForeignKey("FerryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ferry");
                });

            modelBuilder.Entity("FerryBookingClassLibrary.Models.Car", b =>
                {
                    b.Navigation("Guests");
                });

            modelBuilder.Entity("FerryBookingClassLibrary.Models.Ferry", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Guests");
                });
#pragma warning restore 612, 618
        }
    }
}