using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rockaway.WebApp.Migrations {
	/// <inheritdoc />
	public partial class ShowsAndSupportSlots : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Brand",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Product = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Brand", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Show",
				columns: table => new {
					Date = table.Column<DateOnly>(type: "date", nullable: false),
					VenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					HeadlineArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Show", x => new { x.VenueId, x.Date });
					table.ForeignKey(
						name: "FK_Show_Artist_HeadlineArtistId",
						column: x => x.HeadlineArtistId,
						principalTable: "Artist",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Show_Venue_VenueId",
						column: x => x.VenueId,
						principalTable: "Venue",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Endorsement",
				columns: table => new {
					ArtistsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					EndorsementsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Endorsement", x => new { x.ArtistsId, x.EndorsementsId });
					table.ForeignKey(
						name: "FK_Endorsement_Artist_ArtistsId",
						column: x => x.ArtistsId,
						principalTable: "Artist",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Endorsement_Brand_EndorsementsId",
						column: x => x.EndorsementsId,
						principalTable: "Brand",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "SupportSlot",
				columns: table => new {
					SlotNumber = table.Column<int>(type: "int", nullable: false),
					ShowVenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ShowDate = table.Column<DateOnly>(type: "date", nullable: false),
					ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_SupportSlot", x => new { x.ShowVenueId, x.ShowDate, x.SlotNumber });
					table.ForeignKey(
						name: "FK_SupportSlot_Artist_ArtistId",
						column: x => x.ArtistId,
						principalTable: "Artist",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_SupportSlot_Show_ShowVenueId_ShowDate",
						columns: x => new { x.ShowVenueId, x.ShowDate },
						principalTable: "Show",
						principalColumns: new[] { "VenueId", "Date" },
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "rockaway-sample-admin-user",
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "3f041364-0bbf-4d78-9dcb-fbdf0fa31736", "AQAAAAIAAYagAAAAEOr/gM8GAGwI6dwVfNpu1cLrDMENG7nOXhOMi5350A3eYt3OsWj+l9lAe1qayidgWg==", "8b6fe4a9-6e93-4cd1-b762-005144abf87f" });

			migrationBuilder.InsertData(
				table: "Brand",
				columns: new[] { "Id", "Product" },
				values: new object[,]
				{
					{ new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee1"), "Nike" },
					{ new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee2"), "Pingvin" },
					{ new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee3"), "Pepsi-Cola" }
				});

			migrationBuilder.InsertData(
				table: "Show",
				columns: new[] { "Date", "VenueId", "HeadlineArtistId" },
				values: new object[,]
				{
					{ new DateOnly(2024, 8, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 8, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 8, 25), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 8, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 8, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 8, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") },
					{ new DateOnly(2024, 8, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3") }
				});

			migrationBuilder.InsertData(
				table: "Endorsement",
				columns: new[] { "ArtistsId", "EndorsementsId" },
				values: new object[,]
				{
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee1") },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee2") },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee3") },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee1") },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee2") },
					{ new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeee3") }
				});

			migrationBuilder.InsertData(
				table: "SupportSlot",
				columns: new[] { "ShowDate", "ShowVenueId", "SlotNumber", "ArtistId" },
				values: new object[,]
				{
					{ new DateOnly(2024, 8, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa11") },
					{ new DateOnly(2024, 8, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), 2, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa15") },
					{ new DateOnly(2024, 8, 19), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"), 3, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10") },
					{ new DateOnly(2024, 8, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa11") },
					{ new DateOnly(2024, 8, 18), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"), 2, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa15") },
					{ new DateOnly(2024, 8, 25), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10") },
					{ new DateOnly(2024, 8, 25), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"), 2, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa29") },
					{ new DateOnly(2024, 8, 22), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10") },
					{ new DateOnly(2024, 8, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa11") },
					{ new DateOnly(2024, 8, 17), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"), 2, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa15") },
					{ new DateOnly(2024, 8, 23), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10") },
					{ new DateOnly(2024, 8, 20), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"), 1, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa10") }
				});

			migrationBuilder.CreateIndex(
				name: "IX_Endorsement_EndorsementsId",
				table: "Endorsement",
				column: "EndorsementsId");

			migrationBuilder.CreateIndex(
				name: "IX_Show_HeadlineArtistId",
				table: "Show",
				column: "HeadlineArtistId");

			migrationBuilder.CreateIndex(
				name: "IX_SupportSlot_ArtistId",
				table: "SupportSlot",
				column: "ArtistId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Endorsement");

			migrationBuilder.DropTable(
				name: "SupportSlot");

			migrationBuilder.DropTable(
				name: "Brand");

			migrationBuilder.DropTable(
				name: "Show");

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "rockaway-sample-admin-user",
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "c24d9e8a-b26e-40d5-be76-182cf588c781", "AQAAAAIAAYagAAAAEKfMl/qI6N65dhifB5hJp+DN6BgFwtyaX4QcW7DgyPdmBW/RY6y+I7croBi/OdlBYA==", "c5beb9af-e5ef-48ef-a378-becc02c7e64d" });
		}
	}
}