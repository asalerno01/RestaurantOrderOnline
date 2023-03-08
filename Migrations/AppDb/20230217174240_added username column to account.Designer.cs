﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalernoServer.Models;

#nullable disable

namespace SalernoServer.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230217174240_added username column to account")]
    partial class addedusernamecolumntoaccount
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SalernoServer.Models.Account", b =>
                {
                    b.Property<long>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SalernoServer.Models.Addon", b =>
                {
                    b.Property<long>("AddonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ModifierId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("AddonId");

                    b.HasIndex("ModifierId");

                    b.ToTable("Addons");
                });

            modelBuilder.Entity("SalernoServer.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SalernoServer.Models.Group", b =>
                {
                    b.Property<long>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ModifierId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("GroupId");

                    b.HasIndex("ModifierId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SalernoServer.Models.GroupOption", b =>
                {
                    b.Property<long>("GroupOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("GroupOptionId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupOptions");
                });

            modelBuilder.Entity("SalernoServer.Models.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<double>("AssignedCost")
                        .HasColumnType("double");

                    b.Property<string>("Category")
                        .HasColumnType("longtext");

                    b.Property<double>("Cost")
                        .HasColumnType("double");

                    b.Property<string>("Department")
                        .HasColumnType("longtext");

                    b.Property<bool>("Discountable")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ItemUUID")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastSoldDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("LiabilityItem")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LiabilityRedemptionTender")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RecommendedOrder")
                        .HasColumnType("int");

                    b.Property<int>("ReorderTrigger")
                        .HasColumnType("int");

                    b.Property<string>("SKU")
                        .HasColumnType("longtext");

                    b.Property<string>("Secret")
                        .HasColumnType("longtext");

                    b.Property<string>("Supplier")
                        .HasColumnType("longtext");

                    b.Property<string>("TaxGroupOrRate")
                        .HasColumnType("longtext");

                    b.Property<bool>("Taxable")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("TrackingInventory")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UPC")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SalernoServer.Models.Modifier", b =>
                {
                    b.Property<long>("ModifierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ModifierId");

                    b.ToTable("Modifiers");
                });

            modelBuilder.Entity("SalernoServer.Models.NoOption", b =>
                {
                    b.Property<long>("NoOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ModifierId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("NoOptionId");

                    b.HasIndex("ModifierId");

                    b.ToTable("NoOptions");
                });

            modelBuilder.Entity("SalernoServer.Models.Account", b =>
                {
                    b.OwnsMany("SalernoServer.Models.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<long>("AccountId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("AccountId", "Id");

                            b1.ToTable("Accounts_RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");
                        });

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("SalernoServer.Models.Addon", b =>
                {
                    b.HasOne("SalernoServer.Models.Modifier", null)
                        .WithMany("Addons")
                        .HasForeignKey("ModifierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SalernoServer.Models.ApplicationUser", b =>
                {
                    b.OwnsMany("SalernoServer.Models.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<string>("ApplicationUserId")
                                .HasColumnType("varchar(255)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("Token")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("ApplicationUserId", "Id");

                            b1.ToTable("Users_RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("ApplicationUserId");
                        });

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("SalernoServer.Models.Group", b =>
                {
                    b.HasOne("SalernoServer.Models.Modifier", null)
                        .WithMany("Groups")
                        .HasForeignKey("ModifierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SalernoServer.Models.GroupOption", b =>
                {
                    b.HasOne("SalernoServer.Models.Group", null)
                        .WithMany("GroupOptions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SalernoServer.Models.NoOption", b =>
                {
                    b.HasOne("SalernoServer.Models.Modifier", null)
                        .WithMany("NoOptions")
                        .HasForeignKey("ModifierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SalernoServer.Models.Group", b =>
                {
                    b.Navigation("GroupOptions");
                });

            modelBuilder.Entity("SalernoServer.Models.Modifier", b =>
                {
                    b.Navigation("Addons");

                    b.Navigation("Groups");

                    b.Navigation("NoOptions");
                });
#pragma warning restore 612, 618
        }
    }
}
