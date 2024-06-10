using AngleSharp;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using Xunit.Abstractions;

namespace Rockaway.WebApp.Tests.Pages;

public class PageTests {
	private readonly ITestOutputHelper helper;

	public PageTests(ITestOutputHelper helper) {
		this.helper = helper;
	}

	[Fact]
	public async Task Index_Page_Returns_Success() {
		await using var factory = new WebApplicationFactory<Program>();
		using var client = factory.CreateClient();
		using var response = await client.GetAsync("/");
		response.EnsureSuccessStatusCode();
	}

	[Fact]
	public async Task Privacy_Page_Returns_Success() {
		await using var factory = new WebApplicationFactory<Program>();
		using var client = factory.CreateClient();
		using var response = await client.GetAsync("/privacy");
		response.EnsureSuccessStatusCode();
	}



	[Fact]
	public async Task Privacy_Page_Includes_Email_Address() {
		var browsingContext = BrowsingContext.New(Configuration.Default);

		await using var factory = new WebApplicationFactory<Program>();
		using var client = factory.CreateClient();
		var html = await client.GetStringAsync("/privacy");
		var dom = await browsingContext.OpenAsync(req => req.Content(html));
		var privacyLink = dom.QuerySelector("a#privacy-email-link");
		privacyLink.ShouldNotBeNull();
		privacyLink.InnerHtml.ShouldBe("privacy@rockaway.dev");
	}
}