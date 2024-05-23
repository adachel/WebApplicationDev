﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplicationDevSem.DB;

#nullable disable

namespace WebApplicationDevSem.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20240523090702_ProdViewMod")]
    partial class ProdViewMod
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplicationDevSem.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<float>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<int>("ProductGroupId")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("product_pkey");

                    b.HasIndex("ProductGroupId");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("WebApplicationDevSem.Models.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("productgroup_pkey");

                    b.ToTable("productgroups", (string)null);
                });

            modelBuilder.Entity("WebApplicationDevSem.Models.ProductStorage", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("StorageId")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.HasKey("ProductId", "StorageId")
                        .HasName("product_storage_pkey");

                    b.HasIndex("StorageId");

                    b.ToTable("ProductStorage");
                });

            modelBuilder.Entity("WebApplicationDevSem.Models.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("storage_pkey");

                    b.ToTable("storages", (string)null);
                });

            modelBuilder.Entity("WebApplicationDevSem.Models.Product", b =>
                {
                    b.HasOne("WebApplicationDevSem.Models.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductGroup");
                });

            modelBuilder.Entity("WebApplicationDevSem.Models.ProductStorage", b =>
                {
                    b.HasOne("WebApplicationDevSem.Models.Product", "Product")
                        .WithMany("Storages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplicationDevSem.Models.Storage", "Storage")
                        .WithMany("Products")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("WebApplicationDevSem.Models.Product", b =>
                {
                    b.Navigation("Storages");
                });

            modelBuilder.Entity("WebApplicationDevSem.Models.ProductGroup", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebApplicationDevSem.Models.Storage", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
