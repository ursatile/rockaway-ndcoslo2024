// Rockaway.WebApp/Data/RockawayDbContext.cs

using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
// if we want to use Asp.NET to configure our database connection and provider.
public class RockawayDbContext(DbContextOptions<RockawayDbContext> options) : DbContext(options) {

	public DbSet<Artist> Artists { get; set; } = default!;
	public DbSet<Venue> Venues { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		// Override EF Core's default table naming (which pluralizes entity names)
		// and use the same names as the C# classes instead
		foreach (var entity in modelBuilder.Model.GetEntityTypes()) {
			entity.SetTableName(entity.DisplayName());
		}
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Artist>().HasIndex(a => a.Slug).IsUnique();
		modelBuilder.Entity<Venue>().HasIndex(v => v.Slug).IsUnique();
		modelBuilder.Entity<Artist>().HasData(SampleData.Artists.AllArtists);
		modelBuilder.Entity<Venue>().HasData(SampleData.Venues.AllVenues);
	}
}