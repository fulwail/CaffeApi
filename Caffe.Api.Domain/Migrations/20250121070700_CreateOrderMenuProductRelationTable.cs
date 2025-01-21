using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caffe.Api.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrderMenuProductRelationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuProducts_Orders_OrderId",
                table: "MenuProducts");

            migrationBuilder.DropIndex(
                name: "IX_MenuProducts_OrderId",
                table: "MenuProducts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "MenuProducts");

            migrationBuilder.CreateTable(
                name: "OrderMenuProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductMenuId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMenuProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMenuProduct_MenuProducts_ProductMenuId",
                        column: x => x.ProductMenuId,
                        principalTable: "MenuProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderMenuProduct_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderMenuProduct_OrderId",
                table: "OrderMenuProduct",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMenuProduct_ProductMenuId",
                table: "OrderMenuProduct",
                column: "ProductMenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderMenuProduct");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "MenuProducts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuProducts_OrderId",
                table: "MenuProducts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuProducts_Orders_OrderId",
                table: "MenuProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
