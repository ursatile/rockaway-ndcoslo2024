using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Rockaway.WebApp.Services;
using Shouldly;
using Xunit.Abstractions;

namespace Rockaway.WebApp.Tests.Pages;


public class ServiceTests(ITestOutputHelper helper) {
	private static readonly JsonSerializerOptions jsonSerializerOptions
		= new(JsonSerializerDefaults.Web);

	[Fact]
	public async Task StatusEndpoint_Returns_Success_CodeAsync() {
		await using var factory = new WebApplicationFactory<Program>();
		using var client = factory.CreateClient();
		using var response = await client.GetAsync("/status");
		response.EnsureSuccessStatusCode();
	}

	[Fact]
	public async Task StatusEndpoint_Returns_Correct_Details() {
		await using var factory = new WebApplicationFactory<Program>();
		using var client = factory.CreateClient();
		var response = await client.GetStringAsync("/status");
		helper.WriteLine(response);
		var status = JsonSerializer.Deserialize<ServerStatus>(response, jsonSerializerOptions)!;
		// status.Assembly.ShouldBe("Rockaway.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
		status.Hostname.ShouldBe("DYLAN-FRAMEWORK");
	}
}
