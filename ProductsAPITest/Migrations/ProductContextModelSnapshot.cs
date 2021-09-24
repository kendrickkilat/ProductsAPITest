﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsAPITest.Models;

namespace ProductsAPITest.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductsAPITest.Models.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOrdered");

                    b.Property<string>("OrderAddress")
                        .IsRequired();

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ProductsAPITest.Models.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("OrderId");

                    b.Property<decimal>("Price");

                    b.Property<Guid>("ProductId");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ProductsAPITest.Models.Pricing", b =>
                {
                    b.Property<Guid>("PricingId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<decimal>("Price");

                    b.Property<Guid>("ProductId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("PricingId");

                    b.ToTable("Pricings");
                });

            modelBuilder.Entity("ProductsAPITest.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductsAPITest.Models.OrderItem", b =>
                {
                    b.HasOne("ProductsAPITest.Models.Order", "Orders")
                        .WithOne("OrderItem")
                        .HasForeignKey("ProductsAPITest.Models.OrderItem", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProductsAPITest.Models.Product", "Products")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
