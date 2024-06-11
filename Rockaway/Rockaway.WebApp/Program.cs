using Rockaway.WebApp.Components;

using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Mjml.Net;
using RazorEngineCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Hosting;
using Rockaway.WebApp.Services;
using Rockaway.WebApp.Services.Mail;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages(options => options.Conventions.AuthorizeAreaFolder("admin", "/"));
builder.Services.AddControllersWithViews(options => {
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.AddSingleton<IStatusReporter>(new StatusReporter());
builder.Services.AddSingleton<IClock>(SystemClock.Instance);

#if DEBUG
builder.Services.AddSingleton<IMailTemplateProvider>(new DebugMailTemplateProvider());
#else
builder.Services.AddSingleton<IMailTemplateProvider>(new ResourceMailTemplateProvider());
#endif
builder.Services.AddSingleton<IMailBodyRenderer, MailBodyRenderer>();
builder.Services.AddSingleton<IRazorEngine, RazorEngine>();
builder.Services.AddSingleton<IMjmlRenderer, MjmlRenderer>();

builder.Services.AddSingleton<IMailSender, SmtpMailSender>();
var smtpSettings = new SmtpSettings();
builder.Configuration.Bind("Smtp", smtpSettings);
builder.Services.AddSingleton(smtpSettings);
builder.Services.AddSingleton<ISmtpRelay, SmtpRelay>();

#if DEBUG && !NCRUNCH
builder.Services.AddSassCompiler();
#endif
var logger = CreateAdHocLogger<Program>();

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

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

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
app.MapControllers();
app.MapGet("/status", (IStatusReporter reporter) => reporter.GetStatus());

app.MapGet("/api/artists/{slug}/shows", (string slug, RockawayDbContext db) => {
	var artist = db.Artists
		.Include(a => a.HeadlineShows)
		.ThenInclude(show => show.Venue)
		.FirstOrDefault(a => a.Slug == slug);
	return artist.HeadlineShows.Select(s => new {
		Venue = s.Venue.Name,
		Address = s.Venue.FullAddress,
		Date = s.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
	});
});

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();

ILogger<T> CreateAdHocLogger<T>()
	=> LoggerFactory.Create(lb => lb.AddConsole()).CreateLogger<T>();