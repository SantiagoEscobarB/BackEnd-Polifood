using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPolifood.Migrations
{
    public partial class IntegracionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "studentId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "productId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Products_storeId",
                table: "Products",
                column: "storeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_storeId",
                table: "Orders",
                column: "storeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_studentId",
                table: "Orders",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_productId",
                table: "OrderItems",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_storeId",
                table: "Products",
                column: "storeId",
                principalTable: "Stores",
                principalColumn: "storeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stores_storeId",
                table: "Orders",
                column: "storeId",
                principalTable: "Stores",
                principalColumn: "storeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_studentId",
                table: "Orders",
                column: "studentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_productId",
                table: "OrderItems",
                column: "productId",
                principalTable: "Products",
                principalColumn: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_storeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stores_storeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_studentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_productId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Products_storeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_storeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_studentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_productId",
                table: "OrderItems");

            migrationBuilder.AlterColumn<string>(
                name: "studentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "productId",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}