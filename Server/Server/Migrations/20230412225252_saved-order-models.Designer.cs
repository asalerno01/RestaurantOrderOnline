﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalernoServer.Models;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230412225252_saved-order-models")]
    partial class savedordermodels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SalernoServer.Models.Authentication.Account", b =>
                {
                    b.Property<long>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AccountId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SalernoServer.Models.Authentication.Employee", b =>
                {
                    b.Property<long>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("BackOfficeAccess")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmployeeRole")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RegisterCode")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Addon", b =>
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

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Group", b =>
                {
                    b.Property<long>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("ModifierId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("GroupId");

                    b.HasIndex("ModifierId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.GroupOption", b =>
                {
                    b.Property<long>("GroupOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("GroupOptionId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupOptions");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Item", b =>
                {
                    b.Property<string>("ItemId")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("AssignedCost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Discountable")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastSoldDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("LiabilityItem")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LiabilityRedemptionTender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RecommendedOrder")
                        .HasColumnType("int");

                    b.Property<int>("ReorderTrigger")
                        .HasColumnType("int");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TaxGroupOrRate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Taxable")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("TrackingInventory")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UPC")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ItemId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Modifier", b =>
                {
                    b.Property<long>("ModifierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ModifierId");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.ToTable("Modifiers");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.NoOption", b =>
                {
                    b.Property<long>("NoOptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<long>("ModifierId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("NoOptionId");

                    b.HasIndex("ModifierId");

                    b.ToTable("NoOptions");
                });

            modelBuilder.Entity("SalernoServer.Models.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CustomerAccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("PickUpDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("SubtotalTax")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerAccountId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SalernoServer.Models.OrderItem", b =>
                {
                    b.Property<long>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("SalernoServer.Models.OrderItemAddon", b =>
                {
                    b.Property<long>("OrderItemId")
                        .HasColumnType("bigint");

                    b.Property<long>("AddonId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderItemId", "AddonId");

                    b.HasIndex("AddonId");

                    b.ToTable("OrderItemAddons");
                });

            modelBuilder.Entity("SalernoServer.Models.OrderItemGroup", b =>
                {
                    b.Property<long>("OrderItemId")
                        .HasColumnType("bigint");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<long>("GroupOptionId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderItemId", "GroupId", "GroupOptionId");

                    b.HasIndex("GroupId");

                    b.HasIndex("GroupOptionId");

                    b.ToTable("OrderItemGroups");
                });

            modelBuilder.Entity("SalernoServer.Models.OrderItemNoOption", b =>
                {
                    b.Property<long>("OrderItemId")
                        .HasColumnType("bigint");

                    b.Property<long>("NoOptionId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderItemId", "NoOptionId");

                    b.HasIndex("NoOptionId");

                    b.ToTable("OrderItemNoOptions");
                });

            modelBuilder.Entity("Server.Models.Authentication.CustomerAccount", b =>
                {
                    b.Property<long>("CustomerAccountId")
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
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CustomerAccountId");

                    b.ToTable("CustomerAccounts");
                });

            modelBuilder.Entity("Server.Models.ItemModels.Category", b =>
                {
                    b.Property<long>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Server.Models.Review", b =>
                {
                    b.Property<long>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CustomerAccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("CustomerAccountId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Server.Models.SavedOrder", b =>
                {
                    b.Property<long>("CustomerAccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<string>("SavedOrderName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CustomerAccountId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("SavedOrders");
                });

            modelBuilder.Entity("SalernoServer.Models.Authentication.Account", b =>
                {
                    b.HasOne("SalernoServer.Models.Authentication.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Addon", b =>
                {
                    b.HasOne("SalernoServer.Models.ItemModels.Modifier", "Modifier")
                        .WithMany("Addons")
                        .HasForeignKey("ModifierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modifier");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Group", b =>
                {
                    b.HasOne("SalernoServer.Models.ItemModels.Modifier", "Modifier")
                        .WithMany("Groups")
                        .HasForeignKey("ModifierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modifier");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.GroupOption", b =>
                {
                    b.HasOne("SalernoServer.Models.ItemModels.Group", "Group")
                        .WithMany("GroupOptions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Item", b =>
                {
                    b.HasOne("Server.Models.ItemModels.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Modifier", b =>
                {
                    b.HasOne("SalernoServer.Models.ItemModels.Item", "Item")
                        .WithOne("Modifier")
                        .HasForeignKey("SalernoServer.Models.ItemModels.Modifier", "ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.NoOption", b =>
                {
                    b.HasOne("SalernoServer.Models.ItemModels.Modifier", "Modifier")
                        .WithMany("NoOptions")
                        .HasForeignKey("ModifierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modifier");
                });

            modelBuilder.Entity("SalernoServer.Models.Order", b =>
                {
                    b.HasOne("Server.Models.Authentication.CustomerAccount", "CustomerAccount")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerAccount");
                });

            modelBuilder.Entity("SalernoServer.Models.OrderItem", b =>
                {
                    b.HasOne("SalernoServer.Models.ItemModels.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalernoServer.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SalernoServer.Models.OrderItemAddon", b =>
                {
                    b.HasOne("SalernoServer.Models.ItemModels.Addon", "Addon")
                        .WithMany()
                        .HasForeignKey("AddonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalernoServer.Models.OrderItem", "OrderItem")
                        .WithMany("Addons")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Addon");

                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("SalernoServer.Models.OrderItemGroup", b =>
                {
                    b.HasOne("SalernoServer.Models.ItemModels.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalernoServer.Models.ItemModels.GroupOption", "GroupOption")
                        .WithMany()
                        .HasForeignKey("GroupOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalernoServer.Models.OrderItem", "OrderItem")
                        .WithMany("Groups")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("GroupOption");

                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("SalernoServer.Models.OrderItemNoOption", b =>
                {
                    b.HasOne("SalernoServer.Models.ItemModels.NoOption", "NoOption")
                        .WithMany()
                        .HasForeignKey("NoOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalernoServer.Models.OrderItem", "OrderItem")
                        .WithMany("NoOptions")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NoOption");

                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("Server.Models.Review", b =>
                {
                    b.HasOne("Server.Models.Authentication.CustomerAccount", "CustomerAccount")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerAccount");
                });

            modelBuilder.Entity("Server.Models.SavedOrder", b =>
                {
                    b.HasOne("Server.Models.Authentication.CustomerAccount", "CustomerAccount")
                        .WithMany("SavedOrders")
                        .HasForeignKey("CustomerAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalernoServer.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerAccount");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Group", b =>
                {
                    b.Navigation("GroupOptions");
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Item", b =>
                {
                    b.Navigation("Modifier")
                        .IsRequired();
                });

            modelBuilder.Entity("SalernoServer.Models.ItemModels.Modifier", b =>
                {
                    b.Navigation("Addons");

                    b.Navigation("Groups");

                    b.Navigation("NoOptions");
                });

            modelBuilder.Entity("SalernoServer.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("SalernoServer.Models.OrderItem", b =>
                {
                    b.Navigation("Addons");

                    b.Navigation("Groups");

                    b.Navigation("NoOptions");
                });

            modelBuilder.Entity("Server.Models.Authentication.CustomerAccount", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reviews");

                    b.Navigation("SavedOrders");
                });

            modelBuilder.Entity("Server.Models.ItemModels.Category", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
