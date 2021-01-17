﻿// <auto-generated />
using CarRent.Car.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRent.Migrations
{
    [DbContext(typeof(CarDbContext))]
    partial class CarDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CarRent.Car.Domain.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ClassRef")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ClassRef");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("CarRent.Car.Domain.CarClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("PricePerDay")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("class_type")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Class");

                    b.HasDiscriminator<string>("class_type").HasValue("CarClass");
                });

            modelBuilder.Entity("CarRent.Car.Domain.CarSpecification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CarRef")
                        .HasColumnType("int");

                    b.Property<int>("EngineDisplacement")
                        .HasColumnType("int");

                    b.Property<int>("EnginePower")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarRef")
                        .IsUnique();

                    b.ToTable("Specification");
                });

            modelBuilder.Entity("CarRent.Car.Domain.EasyCarClass", b =>
                {
                    b.HasBaseType("CarRent.Car.Domain.CarClass");

                    b.HasDiscriminator().HasValue("class_easy");
                });

            modelBuilder.Entity("CarRent.Car.Domain.LuxuryCarClass", b =>
                {
                    b.HasBaseType("CarRent.Car.Domain.CarClass");

                    b.HasDiscriminator().HasValue("class_luxury");
                });

            modelBuilder.Entity("CarRent.Car.Domain.MediumCarClass", b =>
                {
                    b.HasBaseType("CarRent.Car.Domain.CarClass");

                    b.HasDiscriminator().HasValue("class_medium");
                });

            modelBuilder.Entity("CarRent.Car.Domain.Car", b =>
                {
                    b.HasOne("CarRent.Car.Domain.CarClass", "Class")
                        .WithMany("Cars")
                        .HasForeignKey("ClassRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("CarRent.Car.Domain.CarSpecification", b =>
                {
                    b.HasOne("CarRent.Car.Domain.Car", "Car")
                        .WithOne("Specification")
                        .HasForeignKey("CarRent.Car.Domain.CarSpecification", "CarRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CarRent.Car.Domain.Car", b =>
                {
                    b.Navigation("Specification");
                });

            modelBuilder.Entity("CarRent.Car.Domain.CarClass", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
