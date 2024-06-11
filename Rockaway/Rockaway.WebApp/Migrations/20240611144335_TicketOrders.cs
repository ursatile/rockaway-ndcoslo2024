using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rockaway.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class TicketOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowVenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CompletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketOrder_Show_ShowVenueId_ShowDate",
                        columns: x => new { x.ShowVenueId, x.ShowDate },
                        principalTable: "Show",
                        principalColumns: new[] { "VenueId", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowVenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketType_Show_ShowVenueId_ShowDate",
                        columns: x => new { x.ShowVenueId, x.ShowDate },
                        principalTable: "Show",
                        principalColumns: new[] { "VenueId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketOrderItem",
                columns: table => new
                {
                    TicketOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOrderItem", x => new { x.TicketOrderId, x.TicketTypeId });
                    table.ForeignKey(
                        name: "FK_TicketOrderItem_TicketOrder_TicketOrderId",
                        column: x => x.TicketOrderId,
                        principalTable: "TicketOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketOrderItem_TicketType_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artist",
                columns: new[] { "Id", "Description", "Name", "Slug" },
                values: new object[] { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa30"), "The Linebreakers is the world's greatest internet comedy classic rock disco alt punk covers band.\r\n\r\nLive covers of classic tunes, from ABBA and Adele to Metallica and Nirvana, with the lyrics lovingly reworked to see how many stupid jokes about programming, technology and the web will fit into a four-minute pop song. The tech industry’s answer to Weird Al Yankovic", "The Linebreakers", "the-linebreakers" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "rockaway-sample-admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81a2a86f-acfa-49ef-a299-ec233ebae39c", "AQAAAAIAAYagAAAAEJfFoBS6MeF2aMFZyTh18NUrjRnbe6Fg99D0cDk+jy9/IS52Lb5GSBM0OXPQKhsfxg==", "aab391f5-8b75-442e-bb69-1bcb60dc764d" });

            migrationBuilder.InsertData(
                table: "TicketType",
                columns: new[] { "Id", "Limit", "Name", "Price", "ShowDate", "ShowVenueId" },
                values: new object[,]
                {
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc10"), null, "General Admission", 350m, new DateOnly(2024, 8, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc11"), null, "VIP Meet & Greet", 750m, new DateOnly(2024, 8, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc12"), null, "General Admission", 300m, new DateOnly(2024, 8, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc13"), null, "VIP Meet & Greet", 720m, new DateOnly(2024, 8, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc14"), null, "General Admission", 25m, new DateOnly(2024, 8, 25), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc1"), null, "Upstairs unallocated seating", 25m, new DateOnly(2024, 8, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc2"), null, "Downstairs standing", 25m, new DateOnly(2024, 8, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc3"), null, "Cabaret table (4 people)", 120m, new DateOnly(2024, 8, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc4"), null, "General Admission", 35m, new DateOnly(2024, 8, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc5"), null, "VIP Meet & Greet", 75m, new DateOnly(2024, 8, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc6"), null, "General Admission", 35m, new DateOnly(2024, 8, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc7"), null, "VIP Meet & Greet", 75m, new DateOnly(2024, 8, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc8"), null, "General Admission", 25m, new DateOnly(2024, 8, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9") },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc9"), null, "VIP Meet & Greet", 55m, new DateOnly(2024, 8, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9") }
                });

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                column: "CultureName",
                value: "el-GR");

            migrationBuilder.InsertData(
                table: "Venue",
                columns: new[] { "Id", "Address", "City", "CultureName", "Name", "PostalCode", "Slug", "Telephone", "WebsiteUrl" },
                values: new object[] { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb10"), "Brugata 5", "Oslo", "nb-NO", "Brewgata", "0186", "brewgata-oslo", "+47 48 39 27 57", "https://brewgata.no/" });

            migrationBuilder.InsertData(
                table: "Show",
                columns: new[] { "Date", "VenueId", "HeadlineArtistId" },
                values: new object[] { new DateOnly(2024, 6, 14), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb10"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa30") });

            migrationBuilder.InsertData(
                table: "TicketType",
                columns: new[] { "Id", "Limit", "Name", "Price", "ShowDate", "ShowVenueId" },
                values: new object[] { new Guid("cccccccc-cccc-cccc-cccc-cccccccccc15"), null, "FREE", 0m, new DateOnly(2024, 6, 14), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb10") });

            migrationBuilder.CreateIndex(
                name: "IX_TicketOrder_ShowVenueId_ShowDate",
                table: "TicketOrder",
                columns: new[] { "ShowVenueId", "ShowDate" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketOrderItem_TicketTypeId",
                table: "TicketOrderItem",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketType_ShowVenueId_ShowDate",
                table: "TicketType",
                columns: new[] { "ShowVenueId", "ShowDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketOrderItem");

            migrationBuilder.DropTable(
                name: "TicketOrder");

            migrationBuilder.DropTable(
                name: "TicketType");

            migrationBuilder.DeleteData(
                table: "Show",
                keyColumns: new[] { "Date", "VenueId" },
                keyValues: new object[] { new DateOnly(2024, 6, 14), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb10") });

            migrationBuilder.DeleteData(
                table: "Artist",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa30"));

            migrationBuilder.DeleteData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb10"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "rockaway-sample-admin-user",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c117a2f-0612-4d73-b2d5-e7ba1fb1cdef", "AQAAAAIAAYagAAAAEIbn5nxls2H67oYDIVGpdgtwlD1rB9kMOF3Y+/hbyiJihSnIDYPbDU4vd6ntJG7Hsw==", "a72c70c3-d935-40c7-969d-3d99253b145c" });

            migrationBuilder.UpdateData(
                table: "Venue",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
                column: "CultureName",
                value: "he-GR");
        }
    }
}
