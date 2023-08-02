using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1e4a18b3-a6c5-44fd-a441-114c5b022007"), "Easy" },
                    { new Guid("29d31fc7-1b3a-414a-9a4a-06d5c86f9b5b"), "Hard" },
                    { new Guid("8cdc4fcb-78ae-4942-8429-ead23d6b2209"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("60257737-16a2-4354-a33e-d8ff95009cf3"), "NSN", "Nelson", "nelson-img.png" },
                    { new Guid("68996614-3843-4f4a-867e-d011f1fa2fb4"), "WGN", "Wellington", "wellington-img.png" },
                    { new Guid("a0ddd808-2e24-4ab6-a502-f2b737f67135"), "NTL", "Northland", "northland-img.png" },
                    { new Guid("acdb83cb-3576-40b2-b0a6-594bb5a25302"), "BOP", "Bay of Plenty", "bayOfPlenty-img.png" },
                    { new Guid("e1059975-93dc-4fb3-9eb3-7ed147cb2e3d"), "AKL", "Auckland", "auckland-img.png" },
                    { new Guid("e665ca3b-bed8-403b-905c-f20a241e38a8"), "STL", "Southland", "southland-img.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("1e4a18b3-a6c5-44fd-a441-114c5b022007"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("29d31fc7-1b3a-414a-9a4a-06d5c86f9b5b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8cdc4fcb-78ae-4942-8429-ead23d6b2209"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("60257737-16a2-4354-a33e-d8ff95009cf3"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("68996614-3843-4f4a-867e-d011f1fa2fb4"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a0ddd808-2e24-4ab6-a502-f2b737f67135"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("acdb83cb-3576-40b2-b0a6-594bb5a25302"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e1059975-93dc-4fb3-9eb3-7ed147cb2e3d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e665ca3b-bed8-403b-905c-f20a241e38a8"));
        }
    }
}
