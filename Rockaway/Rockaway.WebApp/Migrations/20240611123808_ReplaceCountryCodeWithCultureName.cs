using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rockaway.WebApp.Migrations {
	/// <inheritdoc />
	public partial class ReplaceCountryCodeWithCultureName : Migration {

		private readonly Dictionary<string, string> countryCodesToCultureNames = new() {
			{ "GB", "en-GB" }, // English (Great Britain)
			{ "FR", "fr-FR" }, // French (France)
			{ "DE", "de-DE" }, // Germany (Germany)
			{ "PT", "pt-PT" }, // Portuguese (Portugal)
			{ "GR", "el-GR" }, // Greek (Greece)
			{ "NO", "nn-NO" }, // Norwegian (Norway)
			{ "SE", "sv-SE" }, // Swedish (Sweden)
			{ "DK", "dk-DK" } // Danish (Denmark)
		};

		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AddColumn<string>(
				name: "CultureName",
				table: "Venue",
				type: "varchar(16)",
				unicode: false,
				maxLength: 16,
				nullable: false,
				defaultValue: "");

			foreach (var (countryCode, cultureName) in countryCodesToCultureNames) {
				var sql = $@"UPDATE Venue
			        SET CultureName = '{cultureName}'
			        WHERE CountryCode = '{countryCode}'";
				migrationBuilder.Sql(sql);
			}


			migrationBuilder.DropColumn(
				name: "CountryCode",
				table: "Venue");


			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "rockaway-sample-admin-user",
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "7c117a2f-0612-4d73-b2d5-e7ba1fb1cdef", "AQAAAAIAAYagAAAAEIbn5nxls2H67oYDIVGpdgtwlD1rB9kMOF3Y+/hbyiJihSnIDYPbDU4vd6ntJG7Hsw==", "a72c70c3-d935-40c7-969d-3d99253b145c" });

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
				column: "CultureName",
				value: "en-GB");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
				column: "CultureName",
				value: "fr-FR");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
				column: "CultureName",
				value: "de-DE");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
				column: "CultureName",
				value: "he-GR");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"),
				column: "CultureName",
				value: "nb-NO");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb6"),
				column: "CultureName",
				value: "da-DK");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"),
				column: "CultureName",
				value: "pt-PT");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"),
				column: "CultureName",
				value: "sv-SE");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"),
				column: "CultureName",
				value: "en-GB");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "CultureName",
				table: "Venue");

			migrationBuilder.AddColumn<string>(
				name: "CountryCode",
				table: "Venue",
				type: "varchar(2)",
				unicode: false,
				maxLength: 2,
				nullable: false,
				defaultValue: "");

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "rockaway-sample-admin-user",
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "3f041364-0bbf-4d78-9dcb-fbdf0fa31736", "AQAAAAIAAYagAAAAEOr/gM8GAGwI6dwVfNpu1cLrDMENG7nOXhOMi5350A3eYt3OsWj+l9lAe1qayidgWg==", "8b6fe4a9-6e93-4cd1-b762-005144abf87f" });

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
				column: "CountryCode",
				value: "GB");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
				column: "CountryCode",
				value: "FR");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
				column: "CountryCode",
				value: "DE");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
				column: "CountryCode",
				value: "GR");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"),
				column: "CountryCode",
				value: "NO");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb6"),
				column: "CountryCode",
				value: "DK");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb7"),
				column: "CountryCode",
				value: "PT");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb8"),
				column: "CountryCode",
				value: "SE");

			migrationBuilder.UpdateData(
				table: "Venue",
				keyColumn: "Id",
				keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb9"),
				column: "CountryCode",
				value: "GB");
		}
	}
}