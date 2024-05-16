﻿// <auto-generated />
using System;
using FerryBookingMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FerryBookingMVC.Migrations
{
    [DbContext(typeof(FerryContext))]
    [Migration("20240515221340_UpdateGuestModel")]
    partial class UpdateGuestModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FerryId = 1
                        },
                        new
                        {
                            Id = 2,
                            FerryId = 1
                        },
                        new
                        {
                            Id = 3,
                            FerryId = 2
                        },
                        new
                        {
                            Id = 4,
                            FerryId = 2
                        });
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerCar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PricePerGuest")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Ferries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MaxCars = 400,
                            MaxGuests = 980,
                            Name = "MOLSLINJEN (Express 4)",
                            PricePerCar = 249m,
                            PricePerGuest = 149m
                        },
                        new
                        {
                            Id = 2,
                            MaxCars = 50,
                            MaxGuests = 100,
                            Name = "Standard Ferry",
                            PricePerCar = 197m,
                            PricePerGuest = 99m
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FerryId = 1,
                            Gender = true,
                            Name = "Alice Smith"
                        },
                        new
                        {
                            Id = 2,
                            FerryId = 1,
                            Gender = false,
                            Name = "Bob Johnson"
                        },
                        new
                        {
                            Id = 3,
                            FerryId = 1,
                            Gender = false,
                            Name = "Charlie Brown"
                        },
                        new
                        {
                            Id = 4,
                            FerryId = 1,
                            Gender = true,
                            Name = "Diana Prince"
                        },
                        new
                        {
                            Id = 5,
                            FerryId = 2,
                            Gender = true,
                            Name = "Eve Davis"
                        },
                        new
                        {
                            Id = 6,
                            FerryId = 2,
                            Gender = false,
                            Name = "Frank Miller"
                        },
                        new
                        {
                            Id = 7,
                            FerryId = 2,
                            Gender = true,
                            Name = "Grace Lee"
                        },
                        new
                        {
                            Id = 8,
                            FerryId = 2,
                            Gender = false,
                            Name = "Hank Green"
                        },
                        new
                        {
                            Id = 9,
                            FerryId = 1,
                            Gender = false,
                            Name = "Isaac Newton"
                        },
                        new
                        {
                            Id = 10,
                            FerryId = 1,
                            Gender = true,
                            Name = "Marie Curie"
                        });
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
                        .OnDelete(DeleteBehavior.Restrict)
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