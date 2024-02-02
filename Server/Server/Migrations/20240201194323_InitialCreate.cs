using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderCount = table.Column<int>(type: "int", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackOfficeAccess = table.Column<bool>(type: "bit", nullable: false),
                    RegisterCode = table.Column<int>(type: "int", nullable: false),
                    EmployeeRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SubtotalTax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuotedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PickupDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedOrders",
                columns: table => new
                {
                    SavedOrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    LastOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedOrders", x => x.SavedOrderId);
                    table.ForeignKey(
                        name: "FK_SavedOrders_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategorySnapshots",
                columns: table => new
                {
                    CategorySnapshotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySnapshots", x => x.CategorySnapshotId);
                    table.ForeignKey(
                        name: "FK_CategorySnapshots_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    UPC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discountable = table.Column<bool>(type: "bit", nullable: false),
                    Taxable = table.Column<bool>(type: "bit", nullable: false),
                    TrackingInventory = table.Column<bool>(type: "bit", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AssignedCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReorderTrigger = table.Column<int>(type: "int", nullable: false),
                    RecommendedOrder = table.Column<int>(type: "int", nullable: false),
                    LastSoldDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LiabilityItem = table.Column<bool>(type: "bit", nullable: false),
                    LiabilityRedemptionTender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxGroupOrRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSnapshots",
                columns: table => new
                {
                    ItemSnapshotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategorySnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    UPC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discountable = table.Column<bool>(type: "bit", nullable: false),
                    Taxable = table.Column<bool>(type: "bit", nullable: false),
                    TrackingInventory = table.Column<bool>(type: "bit", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AssignedCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReorderTrigger = table.Column<int>(type: "int", nullable: false),
                    RecommendedOrder = table.Column<int>(type: "int", nullable: false),
                    LastSoldDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LiabilityItem = table.Column<bool>(type: "bit", nullable: false),
                    LiabilityRedemptionTender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxGroupOrRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSnapshots", x => x.ItemSnapshotId);
                    table.ForeignKey(
                        name: "FK_ItemSnapshots_CategorySnapshots_CategorySnapshotId",
                        column: x => x.CategorySnapshotId,
                        principalTable: "CategorySnapshots",
                        principalColumn: "CategorySnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSnapshots_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modifiers",
                columns: table => new
                {
                    ModifierId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifiers", x => x.ModifierId);
                    table.ForeignKey(
                        name: "FK_Modifiers_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ItemSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_ItemSnapshots_ItemSnapshotId",
                        column: x => x.ItemSnapshotId,
                        principalTable: "ItemSnapshots",
                        principalColumn: "ItemSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addons",
                columns: table => new
                {
                    AddonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addons", x => x.AddonId);
                    table.ForeignKey(
                        name: "FK_Addons_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModifierSnapshots",
                columns: table => new
                {
                    ModifierSnapshotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModifierSnapshots", x => x.ModifierSnapshotId);
                    table.ForeignKey(
                        name: "FK_ModifierSnapshots_ItemSnapshots_ItemSnapshotId",
                        column: x => x.ItemSnapshotId,
                        principalTable: "ItemSnapshots",
                        principalColumn: "ItemSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModifierSnapshots_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NoOptions",
                columns: table => new
                {
                    NoOptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoOptions", x => x.NoOptionId);
                    table.ForeignKey(
                        name: "FK_NoOptions_Modifiers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "Modifiers",
                        principalColumn: "ModifierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedOrderOrderItems",
                columns: table => new
                {
                    SavedOrderOrderItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SavedOrderId = table.Column<long>(type: "bigint", nullable: true),
                    OrderItemId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedOrderOrderItems", x => x.SavedOrderOrderItemId);
                    table.ForeignKey(
                        name: "FK_SavedOrderOrderItems_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemId");
                    table.ForeignKey(
                        name: "FK_SavedOrderOrderItems_SavedOrders_SavedOrderId",
                        column: x => x.SavedOrderId,
                        principalTable: "SavedOrders",
                        principalColumn: "SavedOrderId");
                });

            migrationBuilder.CreateTable(
                name: "GroupOptions",
                columns: table => new
                {
                    GroupOptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOptions", x => x.GroupOptionId);
                    table.ForeignKey(
                        name: "FK_GroupOptions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddonSnapshots",
                columns: table => new
                {
                    AddonSnapshotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddonId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ModifierSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddonSnapshots", x => x.AddonSnapshotId);
                    table.ForeignKey(
                        name: "FK_AddonSnapshots_Addons_AddonId",
                        column: x => x.AddonId,
                        principalTable: "Addons",
                        principalColumn: "AddonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddonSnapshots_ModifierSnapshots_ModifierSnapshotId",
                        column: x => x.ModifierSnapshotId,
                        principalTable: "ModifierSnapshots",
                        principalColumn: "ModifierSnapshotId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupSnapshots",
                columns: table => new
                {
                    GroupSnapshotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifierSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSnapshots", x => x.GroupSnapshotId);
                    table.ForeignKey(
                        name: "FK_GroupSnapshots_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupSnapshots_ModifierSnapshots_ModifierSnapshotId",
                        column: x => x.ModifierSnapshotId,
                        principalTable: "ModifierSnapshots",
                        principalColumn: "ModifierSnapshotId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NoOptionSnapshots",
                columns: table => new
                {
                    NoOptionSnapshotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoOptionId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ModifierSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoOptionSnapshots", x => x.NoOptionSnapshotId);
                    table.ForeignKey(
                        name: "FK_NoOptionSnapshots_ModifierSnapshots_ModifierSnapshotId",
                        column: x => x.ModifierSnapshotId,
                        principalTable: "ModifierSnapshots",
                        principalColumn: "ModifierSnapshotId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NoOptionSnapshots_NoOptions_NoOptionId",
                        column: x => x.NoOptionId,
                        principalTable: "NoOptions",
                        principalColumn: "NoOptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemAddons",
                columns: table => new
                {
                    OrderItemAddonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddonSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    OrderItemId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemAddons", x => x.OrderItemAddonId);
                    table.ForeignKey(
                        name: "FK_OrderItemAddons_AddonSnapshots_AddonSnapshotId",
                        column: x => x.AddonSnapshotId,
                        principalTable: "AddonSnapshots",
                        principalColumn: "AddonSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemAddons_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemId");
                });

            migrationBuilder.CreateTable(
                name: "GroupOptionSnapshots",
                columns: table => new
                {
                    GroupOptionSnapshotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupOptionId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    GroupSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOptionSnapshots", x => x.GroupOptionSnapshotId);
                    table.ForeignKey(
                        name: "FK_GroupOptionSnapshots_GroupOptions_GroupOptionId",
                        column: x => x.GroupOptionId,
                        principalTable: "GroupOptions",
                        principalColumn: "GroupOptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupOptionSnapshots_GroupSnapshots_GroupSnapshotId",
                        column: x => x.GroupSnapshotId,
                        principalTable: "GroupSnapshots",
                        principalColumn: "GroupSnapshotId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemNoOptions",
                columns: table => new
                {
                    OrderItemNoOptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoOptionSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    OrderItemId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemNoOptions", x => x.OrderItemNoOptionId);
                    table.ForeignKey(
                        name: "FK_OrderItemNoOptions_NoOptionSnapshots_NoOptionSnapshotId",
                        column: x => x.NoOptionSnapshotId,
                        principalTable: "NoOptionSnapshots",
                        principalColumn: "NoOptionSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemNoOptions_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemId");
                });

            migrationBuilder.CreateTable(
                name: "OrderItemGroups",
                columns: table => new
                {
                    OrderItemGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupOptionSnapshotId = table.Column<long>(type: "bigint", nullable: false),
                    OrderItemId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemGroups", x => x.OrderItemGroupId);
                    table.ForeignKey(
                        name: "FK_OrderItemGroups_GroupOptionSnapshots_GroupOptionSnapshotId",
                        column: x => x.GroupOptionSnapshotId,
                        principalTable: "GroupOptionSnapshots",
                        principalColumn: "GroupOptionSnapshotId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemGroups_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addons_ModifierId",
                table: "Addons",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_AddonSnapshots_AddonId",
                table: "AddonSnapshots",
                column: "AddonId");

            migrationBuilder.CreateIndex(
                name: "IX_AddonSnapshots_ModifierSnapshotId",
                table: "AddonSnapshots",
                column: "ModifierSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySnapshots_CategoryId",
                table: "CategorySnapshots",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOptions_GroupId",
                table: "GroupOptions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOptionSnapshots_GroupOptionId",
                table: "GroupOptionSnapshots",
                column: "GroupOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOptionSnapshots_GroupSnapshotId",
                table: "GroupOptionSnapshots",
                column: "GroupSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ModifierId",
                table: "Groups",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSnapshots_GroupId",
                table: "GroupSnapshots",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSnapshots_ModifierSnapshotId",
                table: "GroupSnapshots",
                column: "ModifierSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSnapshots_CategorySnapshotId",
                table: "ItemSnapshots",
                column: "CategorySnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSnapshots_ItemId",
                table: "ItemSnapshots",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifiers_ItemId",
                table: "Modifiers",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModifierSnapshots_ItemSnapshotId",
                table: "ModifierSnapshots",
                column: "ItemSnapshotId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModifierSnapshots_ModifierId",
                table: "ModifierSnapshots",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_NoOptions_ModifierId",
                table: "NoOptions",
                column: "ModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_NoOptionSnapshots_ModifierSnapshotId",
                table: "NoOptionSnapshots",
                column: "ModifierSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_NoOptionSnapshots_NoOptionId",
                table: "NoOptionSnapshots",
                column: "NoOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemAddons_AddonSnapshotId",
                table: "OrderItemAddons",
                column: "AddonSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemAddons_OrderItemId",
                table: "OrderItemAddons",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemGroups_GroupOptionSnapshotId",
                table: "OrderItemGroups",
                column: "GroupOptionSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemGroups_OrderItemId",
                table: "OrderItemGroups",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemNoOptions_NoOptionSnapshotId",
                table: "OrderItemNoOptions",
                column: "NoOptionSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemNoOptions_OrderItemId",
                table: "OrderItemNoOptions",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemSnapshotId",
                table: "OrderItems",
                column: "ItemSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountId",
                table: "Orders",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_AccountId",
                table: "Review",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedOrderOrderItems_OrderItemId",
                table: "SavedOrderOrderItems",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedOrderOrderItems_SavedOrderId",
                table: "SavedOrderOrderItems",
                column: "SavedOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedOrders_AccountId",
                table: "SavedOrders",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "OrderItemAddons");

            migrationBuilder.DropTable(
                name: "OrderItemGroups");

            migrationBuilder.DropTable(
                name: "OrderItemNoOptions");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "SavedOrderOrderItems");

            migrationBuilder.DropTable(
                name: "AddonSnapshots");

            migrationBuilder.DropTable(
                name: "GroupOptionSnapshots");

            migrationBuilder.DropTable(
                name: "NoOptionSnapshots");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "SavedOrders");

            migrationBuilder.DropTable(
                name: "Addons");

            migrationBuilder.DropTable(
                name: "GroupOptions");

            migrationBuilder.DropTable(
                name: "GroupSnapshots");

            migrationBuilder.DropTable(
                name: "NoOptions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "ModifierSnapshots");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "ItemSnapshots");

            migrationBuilder.DropTable(
                name: "Modifiers");

            migrationBuilder.DropTable(
                name: "CategorySnapshots");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
