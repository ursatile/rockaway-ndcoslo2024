// Rockaway.WebApp/Data/RockawayDbContext.cs

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
// if we want to use Asp.NET to configure our database connection and provider.
public class RockawayDbContext(DbContextOptions<RockawayDbContext> options)
	: IdentityDbContext<IdentityUser>(options) {

	public DbSet<Artist> Artists { get; set; } = default!;
	public DbSet<Venue> Venues { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		var rockawayEntityNamespace = typeof(Artist).Namespace;
		var rockawayEntities = modelBuilder.Model
			.GetEntityTypes().Where(e =>
				e.ClrType.Namespace == rockawayEntityNamespace);
		foreach (var entity in rockawayEntities) {
			entity.SetTableName(entity.DisplayName());
		}
		modelBuilder.Entity<Artist>().HasIndex(a => a.Slug).IsUnique();
		modelBuilder.Entity<Venue>().HasIndex(v => v.Slug).IsUnique();
		modelBuilder.AddSampleData();
	}
}

public static class DbContextExtensions {
	public static void AddSampleData(this ModelBuilder modelBuilder) {
		modelBuilder.Entity<Artist>().HasData(SampleData.Artists.AllArtists);
		modelBuilder.Entity<Venue>().HasData(SampleData.Venues.AllVenues);
		modelBuilder.Entity<IdentityUser>().HasData(SampleData.Users.Admin);
	}
}