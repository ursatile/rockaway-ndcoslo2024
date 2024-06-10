using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Rockaway.WebApp.Services;
using Shouldly;
using Xunit.Abstractions;

namespace Rockaway.WebApp.Tests.Pages;

public class TestStatusReporter : IStatusReporter {
	public static ServerStatus TestStatus = new() {
		Assembly = "Rockaway.WebApp",
		Modified = "2021-01-01T00:00:00Z",
		Hostname = "TEST-HOSTNAME",
		DateTime = "2021-01-01T02:03:04Z"
	};
	public ServerStatus GetStatus() => TestStatus;
}

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
		var factory = new WebApplicationFactory<Program>()
			.WithWebHostBuilder(builder => builder.ConfigureServices(services => {
				services.AddSingleton<IStatusReporter>(new TestStatusReporter());
			}));
		using var client = factory.CreateClient();
		var response = await client.GetStringAsync("/status");
		helper.WriteLine(response);
		var status = JsonSerializer.Deserialize<ServerStatus>(response, jsonSerializerOptions)!;
		status.Hostname.ShouldBe(TestStatusReporter.TestStatus.Hostname);
		status.DateTime.ShouldBe(TestStatusReporter.TestStatus.DateTime);
		status.Modified.ShouldBe(TestStatusReporter.TestStatus.Modified);
		status.Assembly.ShouldBe(TestStatusReporter.TestStatus.Assembly);
	}
}
