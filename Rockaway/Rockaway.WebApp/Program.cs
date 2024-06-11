using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Hosting;
using Rockaway.WebApp.Services;

var logger = CreateAdHocLogger<Program>();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages(options 
	=> options.Conventions.AuthorizeAreaFolder("admin", "/"));

builder.Services.AddSingleton<IStatusReporter, StatusReporter>();

builder.Services.Configure<RouteOptions>(options
	=> options.LowercaseUrls = true);

if (builder.Environment.UseSqlite()) {
	logger.LogInformation("Using Sqlite database");
	var sqliteConnection = new SqliteConnection("Data Source=:memory:");
	sqliteConnection.Open();
	builder.Services.AddDbContext<RockawayDbContext>(options
		=> options.UseSqlite(sqliteConnection));
} else {
	logger.LogInformation("Using SQL Server database");
	var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
	builder.Services.AddDbContext<RockawayDbContext>(options => options.UseSqlServer(connectionString));

}

builder.Services
	.AddDefaultIdentity<IdentityUser>()
	.AddEntityFrameworkStores<RockawayDbContext>();

#if DEBUG && !NCRUNCH
builder.Services.AddSassCompiler();
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

using var scope = app.Services.CreateScope();
using var db = scope.ServiceProvider.GetService<RockawayDbContext>()!;
if (app.Environment.UseSqlite()) {
	logger.LogInformation("Using Sqlite - calling Database.EnsureCreated()");
	db.Database.EnsureCreated();
} else {
	logger.LogInformation("Using SQL Server - calling Database.Migrate()");
	db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapAreaControllerRoute(
	name: "admin",
	areaName: "Admin",
	pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
).RequireAuthorization();

app.MapRazorPages();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.MapGet("/status", (IStatusReporter reporter) => reporter.GetStatus());

app.Run();

ILogger<T> CreateAdHocLogger<T>()
	=> LoggerFactory.Create(lb => lb.AddConsole()).CreateLogger<T>();