using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TradeMarket.Migrations
{
    /// <inheritdoc />
    public partial class ModelRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_RequestedProduct_AskingForId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SellerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestedProduct_RequestedProduct_AskingForId",
                table: "RequestedProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestedProduct_Users_SellerId",
                table: "RequestedProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "RequestedProduct",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AskingForId",
                table: "RequestedProduct",
                newName: "AskingForRequestedProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RequestedProduct",
                newName: "RequestedProductId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestedProduct_SellerId",
                table: "RequestedProduct",
                newName: "IX_RequestedProduct_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestedProduct_AskingForId",
                table: "RequestedProduct",
                newName: "IX_RequestedProduct_AskingForRequestedProductId");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Products",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AskingForId",
                table: "Products",
                newName: "RequestedProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_AskingForId",
                table: "Products",
                newName: "IX_Products_RequestedProductId");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "Users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Instant>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "LastUpdated",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "CreatedAt",
                table: "RequestedProduct",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "LastUpdated",
                table: "RequestedProduct",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "CreatedAt",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "LastUpdated",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Address",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Instant>(
                name: "CreatedAt",
                table: "Address",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Instant>(
                name: "LastUpdated",
                table: "Address",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Address",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Users_UserId",
                table: "Address",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_RequestedProduct_RequestedProductId",
                table: "Products",
                column: "RequestedProductId",
                principalTable: "RequestedProduct",
                principalColumn: "RequestedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedProduct_RequestedProduct_AskingForRequestedProduct~",
                table: "RequestedProduct",
                column: "AskingForRequestedProductId",
                principalTable: "RequestedProduct",
                principalColumn: "RequestedProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedProduct_Users_UserId",
                table: "RequestedProduct",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Users_UserId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_RequestedProduct_RequestedProductId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestedProduct_RequestedProduct_AskingForRequestedProduct~",
                table: "RequestedProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestedProduct_Users_UserId",
                table: "RequestedProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_UserId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RequestedProduct");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "RequestedProduct");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RequestedProduct",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "AskingForRequestedProductId",
                table: "RequestedProduct",
                newName: "AskingForId");

            migrationBuilder.RenameColumn(
                name: "RequestedProductId",
                table: "RequestedProduct",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_RequestedProduct_UserId",
                table: "RequestedProduct",
                newName: "IX_RequestedProduct_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestedProduct_AskingForRequestedProductId",
                table: "RequestedProduct",
                newName: "IX_RequestedProduct_AskingForId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Products",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "RequestedProductId",
                table: "Products",
                newName: "AskingForId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Products",
                newName: "IX_Products_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_RequestedProductId",
                table: "Products",
                newName: "IX_Products_AskingForId");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Address",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_RequestedProduct_AskingForId",
                table: "Products",
                column: "AskingForId",
                principalTable: "RequestedProduct",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedProduct_RequestedProduct_AskingForId",
                table: "RequestedProduct",
                column: "AskingForId",
                principalTable: "RequestedProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedProduct_Users_SellerId",
                table: "RequestedProduct",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
