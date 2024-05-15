﻿// <auto-generated />
using System;
using FerryBookingMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FerryBookingMVC.Migrations
{
    [DbContext(typeof(FerryContext))]
    partial class FerryContextModelSnapshot : ModelSnapshot
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

                    b.Property<int?>("FerryId")
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

                    b.Property<decimal>("CarPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("GuestPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<int>("MaxGuests")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Ferries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CarPrice = 197m,
                            GuestPrice = 99m,
                            Length = 10,
                            MaxGuests = 40
                        },
                        new
                        {
                            Id = 2,
                            CarPrice = 197m,
                            GuestPrice = 99m,
                            Length = 20,
                            MaxGuests = 80
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

                    b.Property<int?>("FerryId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("FerryId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("FerryBookingClassLibrary.Models.Car", b =>
                {
                    b.HasOne("FerryBookingClassLibrary.Models.Ferry", null)
                        .WithMany("Cars")
                        .HasForeignKey("FerryId");
                });

            modelBuilder.Entity("FerryBookingClassLibrary.Models.Guest", b =>
                {
                    b.HasOne("FerryBookingClassLibrary.Models.Car", null)
                        .WithMany("Guests")
                        .HasForeignKey("CarId");

                    b.HasOne("FerryBookingClassLibrary.Models.Ferry", null)
                        .WithMany("Guests")
                        .HasForeignKey("FerryId");
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
