﻿// <auto-generated />
using System;
using ECom.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECom.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241120183326_Order_OrderList_Tables")]
    partial class Order_OrderList_Tables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ECom.Data.Account.EComRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("ECom.Data.Account.EComUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AddressDelivery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ECom.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("OrderListId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id", "ProductId");

                    b.HasIndex("OrderListId");

                    b.HasIndex("ProductId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ECom.Data.Models.OrderList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsFinalized")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("OrderLists");
                });

            modelBuilder.Entity("ECom.Data.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Background")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateOnly>("DateCreated")
                        .HasColumnType("date");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Platform")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalRating")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.HasIndex("DateCreated")
                        .IsDescending();

                    b.HasIndex("Genre");

                    b.HasIndex("Name");

                    b.HasIndex("Platform");

                    b.HasIndex("TotalRating")
                        .IsDescending();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Count = 100,
                            DateCreated = new DateOnly(2021, 1, 1),
                            Genre = "Action",
                            IsDeleted = false,
                            Name = "PC game 1",
                            Platform = 1,
                            Price = 50m,
                            Rating = 0,
                            TotalRating = 4.5m
                        },
                        new
                        {
                            Id = 2,
                            Count = 200,
                            DateCreated = new DateOnly(2021, 1, 2),
                            Genre = "Adventure",
                            IsDeleted = false,
                            Name = "PC game 2",
                            Platform = 1,
                            Price = 60m,
                            Rating = 1,
                            TotalRating = 4.6m
                        },
                        new
                        {
                            Id = 3,
                            Count = 300,
                            DateCreated = new DateOnly(2021, 1, 3),
                            Genre = "RPG",
                            IsDeleted = false,
                            Name = "PC game 3",
                            Platform = 1,
                            Price = 70m,
                            Rating = 2,
                            TotalRating = 4.7m
                        },
                        new
                        {
                            Id = 4,
                            Count = 400,
                            DateCreated = new DateOnly(2018, 1, 4),
                            Genre = "Puzzle",
                            IsDeleted = false,
                            Name = "Mobile game 1",
                            Platform = 2,
                            Price = 30m,
                            Rating = 2,
                            TotalRating = 4.8m
                        },
                        new
                        {
                            Id = 5,
                            Count = 500,
                            DateCreated = new DateOnly(2021, 1, 5),
                            Genre = "Strategy",
                            IsDeleted = false,
                            Name = "Mobile game 2",
                            Platform = 2,
                            Price = 40m,
                            Rating = 3,
                            TotalRating = 4.9m
                        },
                        new
                        {
                            Id = 6,
                            Count = 600,
                            DateCreated = new DateOnly(2020, 1, 6),
                            Genre = "Simulation",
                            IsDeleted = false,
                            Name = "Mobile game 3",
                            Platform = 2,
                            Price = 50m,
                            Rating = 3,
                            TotalRating = 5.0m
                        },
                        new
                        {
                            Id = 7,
                            Count = 700,
                            DateCreated = new DateOnly(2021, 1, 7),
                            Genre = "Sports",
                            IsDeleted = false,
                            Name = "Console game 1",
                            Platform = 0,
                            Price = 60m,
                            Rating = 3,
                            TotalRating = 4.1m
                        },
                        new
                        {
                            Id = 8,
                            Count = 800,
                            DateCreated = new DateOnly(2021, 1, 8),
                            Genre = "Racing",
                            IsDeleted = false,
                            Name = "Console game 2",
                            Platform = 0,
                            Price = 70m,
                            Rating = 4,
                            TotalRating = 4.2m
                        },
                        new
                        {
                            Id = 9,
                            Count = 900,
                            DateCreated = new DateOnly(2021, 1, 9),
                            Genre = "Horror",
                            IsDeleted = false,
                            Name = "VR game 1",
                            Platform = 3,
                            Price = 80m,
                            Rating = 4,
                            TotalRating = 4.3m
                        },
                        new
                        {
                            Id = 10,
                            Count = 1000,
                            DateCreated = new DateOnly(2023, 1, 10),
                            Genre = "MMO",
                            IsDeleted = false,
                            Name = "Web game 1",
                            Platform = 4,
                            Price = 90m,
                            Rating = 4,
                            TotalRating = 4.4m
                        });
                });

            modelBuilder.Entity("ECom.Data.Models.ProductRating", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductRatings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ECom.Data.Models.Order", b =>
                {
                    b.HasOne("ECom.Data.Models.OrderList", "OrderList")
                        .WithMany("Orders")
                        .HasForeignKey("OrderListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECom.Data.Models.Product", "Product")
                        .WithMany("AssociatedOrders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderList");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ECom.Data.Models.OrderList", b =>
                {
                    b.HasOne("ECom.Data.Account.EComUser", "Customer")
                        .WithMany("OrderLists")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ECom.Data.Models.ProductRating", b =>
                {
                    b.HasOne("ECom.Data.Models.Product", "Product")
                        .WithMany("Ratings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECom.Data.Account.EComUser", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("ECom.Data.Account.EComRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("ECom.Data.Account.EComUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("ECom.Data.Account.EComUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("ECom.Data.Account.EComRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECom.Data.Account.EComUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("ECom.Data.Account.EComUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ECom.Data.Account.EComUser", b =>
                {
                    b.Navigation("OrderLists");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("ECom.Data.Models.OrderList", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ECom.Data.Models.Product", b =>
                {
                    b.Navigation("AssociatedOrders");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
