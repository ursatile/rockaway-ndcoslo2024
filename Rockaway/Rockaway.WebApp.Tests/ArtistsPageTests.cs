using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Tests.Pages;

public class ArtistTests {

	public static IEnumerable<object[]> Artists =>
		SampleData.Artists.AllArtists
			.Select(a => new object[] { a })
			.ToArray();

	[Theory]
	[MemberData(nameof(Artists))]
	public async Task Artist_Detail_Page_Contains_Artist(Artist artist) {
		await using var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var html = await client.GetStringAsync($"/artists/{artist.Slug}");
		var decodedHtml = WebUtility.HtmlDecode(html);
		decodedHtml.ShouldContain(artist.Name);
		decodedHtml.ShouldContain(artist.Description);
	}

	[Theory]
	[MemberData(nameof(Artists))]
	public async Task Artist_List_Page_Contains_Artist(Artist artist) {
		await using var factory = new WebApplicationFactory<Program>();
		var client = factory.CreateClient();
		var html = await client.GetStringAsync("/artists");
		var decodedHtml = WebUtility.HtmlDecode(html);
		decodedHtml.ShouldContain(artist.Name);
	}

	// [Fact]
	// public async Task Artist_Page_Contains_All_Artists() {
	// 	await using var factory = new WebApplicationFactory<Program>();
	// 	var client = factory.CreateClient();
	// 	var html = await client.GetStringAsync("/artists");
	// 	var decodedHtml = WebUtility.HtmlDecode(html);
	// 	using var scope = factory.Services.CreateScope();
	// 	var db = scope.ServiceProvider.GetService<RockawayDbContext>()!;
	// 	var expected = db.Artists.ToList();
	// 	foreach (var artist in expected) decodedHtml.ShouldContain(artist.Name);
	// }
}

