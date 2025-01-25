using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caffe.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "MenuProducts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitorName = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentType = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuProducts_Orders_OrderId",
                table: "MenuProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_MenuProducts_OrderId",
                table: "MenuProducts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "MenuProducts");
        }
    }
}
