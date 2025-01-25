using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caffe.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateMenuProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuProducts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuProducts_Name",
                table: "MenuProducts",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuProducts");
        }
    }
}
