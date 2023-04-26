﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OrderService.Data;

#nullable disable

namespace OrderService.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    partial class OrderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OrderService.Data.Entities.Buyer", b =>
                {
                    b.Property<Guid>("BuyerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BuyerId");

                    b.ToTable("Buyers");

                    b.HasData(
                        new
                        {
                            BuyerId = new Guid("080ffe61-e23c-4ec3-b851-8e2ad61e45b2"),
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 4, 25, 12, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                            CustomerId = new Guid("d6ed1b92-6d18-4643-97b8-1c308eb26c2e"),
                            Lastname = "Yıl",
                            Name = "Sinan"
                        });
                });

            modelBuilder.Entity("OrderService.Data.Entities.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = new Guid("eccaa41a-2575-4d13-a9a3-158fd2bb2bd7"),
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 4, 25, 12, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                            CustomerId = new Guid("d6ed1b92-6d18-4643-97b8-1c308eb26c2e"),
                            Status = 10,
                            TotalAmount = 15.37m
                        });
                });

            modelBuilder.Entity("OrderService.Data.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            OrderItemId = new Guid("25920939-2175-4084-992a-753e162d48f0"),
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 4, 25, 12, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                            OrderId = new Guid("eccaa41a-2575-4d13-a9a3-158fd2bb2bd7"),
                            ProductId = new Guid("515718c9-4f7f-4014-9b54-8cd3c32e08d9"),
                            ProductName = "White Eraser",
                            Quantity = 2,
                            TotalPrice = 7.38m,
                            UnitPrice = 3.69m
                        },
                        new
                        {
                            OrderItemId = new Guid("925bf9c3-890e-4c45-b80c-8f471172f7e8"),
                            CreatedAt = new DateTimeOffset(new DateTime(2023, 4, 25, 12, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                            OrderId = new Guid("eccaa41a-2575-4d13-a9a3-158fd2bb2bd7"),
                            ProductId = new Guid("80210541-59fb-41a3-af1b-cce95c06c829"),
                            ProductName = "Red Notepad",
                            Quantity = 1,
                            TotalPrice = 7.99m,
                            UnitPrice = 7.99m
                        });
                });

            modelBuilder.Entity("OrderService.Data.Entities.Order", b =>
                {
                    b.HasOne("OrderService.Data.Entities.Buyer", "Buyer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasPrincipalKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("OrderService.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("OrderService.Data.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OrderService.Data.Entities.Buyer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("OrderService.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}