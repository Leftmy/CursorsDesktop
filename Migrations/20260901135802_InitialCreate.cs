using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CursorsDesktop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CursorTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SystemRole = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PackageName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    PathToPreview = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PackageId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PathToIcon = table.Column<string>(type: "TEXT", nullable: false),
                    PathToPreview = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursors_CursorTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CursorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cursors_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CursorTypes",
                columns: new[] { "Id", "Name", "SystemRole" },
                values: new object[,]
                {
                    { 1, "Default Pointer", "Arrow" },
                    { 2, "Help", "Help" },
                    { 3, "Working in Background", "AppStarting" },
                    { 4, "Busy", "Wait" },
                    { 5, "Precision Select", "Crosshair" },
                    { 6, "Text Selection", "IBeam" },
                    { 7, "Handwriting", "NWPen" },
                    { 8, "Not Allowed", "No" },
                    { 9, "Resize Vertical", "SizeNS" },
                    { 10, "Resize Horizontal", "SizeWE" },
                    { 11, "Diagonal Resize 1", "SizeNWSE" },
                    { 12, "Diagonal Resize 2", "SizeNESW" },
                    { 13, "Move", "SizeAll" },
                    { 14, "Alternate Select", "UpArrow" },
                    { 15, "Link Select", "Hand" },
                    { 16, "Location Select", "Pin" },
                    { 17, "Person Select", "Person" },
                    { 18, "Auto-scroll (All Directions)", "PanAll" },
                    { 19, "Auto-scroll (North-South)", "PanNS" },
                    { 20, "Auto-scroll (West-East)", "PanWE" },
                    { 21, "Auto-scroll (North)", "PanN" },
                    { 22, "Auto-scroll (South)", "PanS" },
                    { 23, "Auto-scroll (West)", "PanW" },
                    { 24, "Auto-scroll (East)", "PanE" },
                    { 25, "Auto-scroll (North West)", "PanNW" },
                    { 26, "Auto-scroll (North East)", "PanNE" },
                    { 27, "Auto-scroll (South West)", "PanSW" },
                    { 28, "Auto-scroll (South East)", "PanSE" },
                    { 29, "CD Auto-run", "CD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursors_PackageId",
                table: "Cursors",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursors_TypeId",
                table: "Cursors",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CursorTypes_Name",
                table: "CursorTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CursorTypes_SystemRole",
                table: "CursorTypes",
                column: "SystemRole",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_PackageName",
                table: "Packages",
                column: "PackageName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursors");

            migrationBuilder.DropTable(
                name: "CursorTypes");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
