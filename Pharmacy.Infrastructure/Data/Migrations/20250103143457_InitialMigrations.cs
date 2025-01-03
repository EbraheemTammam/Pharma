using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pharmacy.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename tables
            migrationBuilder.RenameTable(name: "accounts_customuser", newName: "Users");
            migrationBuilder.RenameTable(name: "auth_group", newName: "Roles");
            migrationBuilder.RenameTable(name: "django_content_type", newName: "ContentTypes");
            migrationBuilder.RenameTable(name: "finance_company", newName: "ProductProvider");
            migrationBuilder.RenameTable(name: "orders_customer", newName: "Customer");
            migrationBuilder.RenameTable(name: "products_scarce", newName: "ScarceProduct");
            migrationBuilder.RenameTable(name: "products_type", newName: "Product");
            migrationBuilder.RenameTable(name: "finance_incomingorder", newName: "IncomingOrder");
            migrationBuilder.RenameTable(name: "orders_order", newName: "Order");
            migrationBuilder.RenameTable(name: "orders_payment", newName: "Payment");
            migrationBuilder.RenameTable(name: "orders_orderitem", newName: "OrderItem");
            migrationBuilder.RenameTable(name: "products_product", newName: "ProductItem");

            // Rename columns in Users table
            migrationBuilder.RenameColumn(table: "Users", name: "id", newName: "Id");
            migrationBuilder.RenameColumn(table: "Users", name: "date_joined", newName: "CreatedAt");
            migrationBuilder.RenameColumn(table: "Users", name: "email", newName: "Email");
            migrationBuilder.RenameColumn(table: "Users", name: "username", newName: "UserName");
            migrationBuilder.RenameColumn(table: "Users", name: "first_name", newName: "FirstName");
            migrationBuilder.RenameColumn(table: "Users", name: "last_name", newName: "LastName");
            migrationBuilder.RenameColumn(table: "Users", name: "is_active", newName: "IsActive");
            migrationBuilder.RenameColumn(table: "Users", name: "is_admin", newName: "IsAdmin");
            migrationBuilder.RenameColumn(table: "Users", name: "is_staff", newName: "IsStaff");
            migrationBuilder.RenameColumn(table: "Users", name: "is_superuser", newName: "IsSuperuser");
            migrationBuilder.RenameColumn(table: "Users", name: "last_login", newName: "LastLogin");
            migrationBuilder.RenameColumn(table: "Users", name: "password", newName: "PasswordHash");

            // Add missing columns to Users table
            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsStaff",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsSuperuser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            // Rename columns in Roles table
            migrationBuilder.RenameColumn(table: "Roles", name: "id", newName: "Id");
            migrationBuilder.RenameColumn(table: "Roles", name: "name", newName: "Name");

            // User Claims
            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Add missing columns to Roles table
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "Roles",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Roles",
                type: "text",
                nullable: true);

            // Role Claims
            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Rename columns in Customer table
            migrationBuilder.RenameColumn(table: "Customer", name: "id", newName: "Id");
            migrationBuilder.RenameColumn(table: "Customer", name: "dept", newName: "Dept");
            migrationBuilder.RenameColumn(table: "Customer", name: "name", newName: "Name");

            // Rename columns in Product table
            migrationBuilder.RenameColumn(table: "Product", name: "id", newName: "Id");
            migrationBuilder.RenameColumn(table: "Product", name: "barcode", newName: "Barcode");
            migrationBuilder.RenameColumn(table: "Product", name: "lack", newName: "IsLack");
            migrationBuilder.RenameColumn(table: "Product", name: "minimum", newName: "Minimum");
            migrationBuilder.RenameColumn(table: "Product", name: "name", newName: "Name");
            migrationBuilder.RenameColumn(table: "Product", name: "number_of_elements", newName: "NumberOfElements");
            migrationBuilder.RenameColumn(table: "Product", name: "owned_elements", newName: "OwnedElements");
            migrationBuilder.RenameColumn(table: "Product", name: "price_per_element", newName: "PricePerElement");

            // Rename columns in ProductProvider table
            migrationBuilder.RenameColumn(table: "ProductProvider", name: "id", newName: "Id");
            migrationBuilder.RenameColumn(table: "ProductProvider", name: "name", newName: "Name");
            migrationBuilder.AddColumn<double>(
                name: "Indepted",
                table: "ProductProvider",
                type: "double precision",
                nullable: true);

            // Rename columns in IncomingOrder table
            migrationBuilder.RenameColumn(table: "IncomingOrder", name: "id", newName: "Id");
            migrationBuilder.RenameColumn(table: "IncomingOrder", name: "paid", newName: "Paid");
            migrationBuilder.RenameColumn(table: "IncomingOrder", name: "price", newName: "Price");
            migrationBuilder.RenameColumn(table: "IncomingOrder", name: "time", newName: "CreatedAt");

            // Rename columns in Order table
            migrationBuilder.RenameColumn(table: "Order", name: "id", newName: "Id");
            migrationBuilder.RenameColumn(table: "Order", name: "paid", newName: "Paid");
            migrationBuilder.RenameColumn(table: "Order", name: "time", newName: "CreatedAt");
            migrationBuilder.RenameColumn(table: "Order", name: "total_price", newName: "TotalPrice");

            // Rename columns in Payment table
            migrationBuilder.RenameColumn(table: "Payment", name: "id", newName: "Id");
            migrationBuilder.RenameColumn(table: "Payment", name: "paid", newName: "Paid");
            migrationBuilder.RenameColumn(table: "Payment", name: "time", newName: "CreatedAt");

            // Rename columns in OrderItem table
            migrationBuilder.RenameColumn(table: "OrderItem", name: "id", newName: "Id");
            migrationBuilder.RenameColumn(table: "OrderItem", name: "amount", newName: "Amount");
            migrationBuilder.RenameColumn(table: "OrderItem", name: "price", newName: "Price");

            // Rename columns in ProductItem table
            migrationBuilder.RenameColumn(table: "ProductItem", name: "id", newName: "Id");
            migrationBuilder.RenameColumn(table: "ProductItem", name: "expiration", newName: "ExpirationDate");
            migrationBuilder.RenameColumn(table: "ProductItem", name: "number_of_boxes", newName: "NumberOfBoxes");
            migrationBuilder.RenameColumn(table: "ProductItem", name: "number_of_elements", newName: "NumberOfElements");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert table renames
            migrationBuilder.RenameTable(name: "Users", newName: "accounts_customuser");
            migrationBuilder.RenameTable(name: "Roles", newName: "auth_group");
            migrationBuilder.RenameTable(name: "ContentTypes", newName: "django_content_type");
            migrationBuilder.RenameTable(name: "ProductProvider", newName: "finance_company");
            migrationBuilder.RenameTable(name: "Customer", newName: "orders_customer");
            migrationBuilder.RenameTable(name: "ScarceProduct", newName: "products_scarce");
            migrationBuilder.RenameTable(name: "Product", newName: "products_type");
            migrationBuilder.RenameTable(name: "IncomingOrder", newName: "finance_incomingorder");
            migrationBuilder.RenameTable(name: "Order", newName: "orders_order");
            migrationBuilder.RenameTable(name: "Payment", newName: "orders_payment");
            migrationBuilder.RenameTable(name: "OrderItem", newName: "orders_orderitem");
            migrationBuilder.RenameTable(name: "ProductItem", newName: "products_product");

            // Revert column renames in Users table
            migrationBuilder.RenameColumn(table: "accounts_customuser", name: "CreatedAt", newName: "date_joined");
            migrationBuilder.RenameColumn(table: "accounts_customuser", name: "FirstName", newName: "first_name");
            migrationBuilder.RenameColumn(table: "accounts_customuser", name: "LastName", newName: "last_name");
            migrationBuilder.RenameColumn(table: "accounts_customuser", name: "IsActive", newName: "is_active");
            migrationBuilder.RenameColumn(table: "accounts_customuser", name: "IsAdmin", newName: "is_admin");
            migrationBuilder.RenameColumn(table: "accounts_customuser", name: "IsStaff", newName: "is_staff");
            migrationBuilder.RenameColumn(table: "accounts_customuser", name: "IsSuperuser", newName: "is_superuser");
            migrationBuilder.RenameColumn(table: "accounts_customuser", name: "LastLogin", newName: "last_login");
            migrationBuilder.RenameColumn(table: "accounts_customuser", name: "PasswordHash", newName: "password");

            // Drop added columns in Users table
            migrationBuilder.DropColumn(name: "NormalizedEmail", table: "accounts_customuser");
            migrationBuilder.DropColumn(name: "NormalizedUserName", table: "accounts_customuser");
            migrationBuilder.DropColumn(name: "ConcurrencyStamp", table: "accounts_customuser");
            migrationBuilder.DropColumn(name: "SecurityStamp", table: "accounts_customuser");
            migrationBuilder.DropColumn(name: "TwoFactorEnabled", table: "accounts_customuser");
            migrationBuilder.DropColumn(name: "LockoutEnd", table: "accounts_customuser");
            migrationBuilder.DropColumn(name: "LockoutEnabled", table: "accounts_customuser");
            migrationBuilder.DropColumn(name: "AccessFailedCount", table: "accounts_customuser");

            // Revert column renames in Roles table
            migrationBuilder.RenameColumn(table: "auth_group", name: "Name", newName: "name");

            // Revert column renames in Customer table
            migrationBuilder.RenameColumn(table: "orders_customer", name: "Dept", newName: "dept");
            migrationBuilder.RenameColumn(table: "orders_customer", name: "Name", newName: "name");

            // Revert column renames in Product table
            migrationBuilder.RenameColumn(table: "products_type", name: "Barcode", newName: "barcode");
            migrationBuilder.RenameColumn(table: "products_type", name: "IsLack", newName: "lack");
            migrationBuilder.RenameColumn(table: "products_type", name: "Minimum", newName: "minimum");
            migrationBuilder.RenameColumn(table: "products_type", name: "Name", newName: "name");
            migrationBuilder.RenameColumn(table: "products_type", name: "NumberOfElements", newName: "number_of_elements");
            migrationBuilder.RenameColumn(table: "products_type", name: "OwnedElements", newName: "owned_elements");
            migrationBuilder.RenameColumn(table: "products_type", name: "PricePerElement", newName: "price_per_element");

            // Revert column renames in ProductProvider table
            migrationBuilder.RenameColumn(table: "finance_company", name: "Name", newName: "name");
            migrationBuilder.RenameColumn(table: "finance_company", name: "Indepted", newName: "indepted");

            // Revert column renames in IncomingOrder table
            migrationBuilder.RenameColumn(table: "finance_incomingorder", name: "Paid", newName: "paid");
            migrationBuilder.RenameColumn(table: "finance_incomingorder", name: "Price", newName: "price");
            migrationBuilder.RenameColumn(table: "finance_incomingorder", name: "CreatedAt", newName: "time");

            // Revert column renames in Order table
            migrationBuilder.RenameColumn(table: "orders_order", name: "Paid", newName: "paid");
            migrationBuilder.RenameColumn(table: "orders_order", name: "CreatedAt", newName: "time");
            migrationBuilder.RenameColumn(table: "orders_order", name: "TotalPrice", newName: "total_price");

            // Revert column renames in Payment table
            migrationBuilder.RenameColumn(table: "orders_payment", name: "Paid", newName: "paid");
            migrationBuilder.RenameColumn(table: "orders_payment", name: "CreatedAt", newName: "time");

            // Revert column renames in OrderItem table
            migrationBuilder.RenameColumn(table: "orders_orderitem", name: "Amount", newName: "amount");
            migrationBuilder.RenameColumn(table: "orders_orderitem", name: "Price", newName: "price");

            // Revert column renames in ProductItem table
            migrationBuilder.RenameColumn(table: "products_product", name: "ExpirationDate", newName: "expiration");
            migrationBuilder.RenameColumn(table: "products_product", name: "NumberOfBoxes", newName: "number_of_boxes");
            migrationBuilder.RenameColumn(table: "products_product", name: "NumberOfElements", newName: "number_of_elements");
        }
    }
}
