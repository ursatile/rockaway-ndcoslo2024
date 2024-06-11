// Rockaway.WebApp/Data/RockawayDbContext.cs

using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
// if we want to use Asp.NET to configure our database connection and provider.
public class RockawayDbContext(DbContextOptions<RockawayDbContext> options)
	: IdentityDbContext<IdentityUser>(options) {

	public DbSet<Artist> Artists { get; set; } = default!;
	public DbSet<Venue> Venues { get; set; } = default!;
	public DbSet<Show> Shows { get; set; } = default!;
	public DbSet<Brand> Brands { get; set; } = default!;

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
		base.ConfigureConventions(configurationBuilder);
		configurationBuilder.AddNodaTimeConverters();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		var rockawayEntityNamespace = typeof(Artist).Namespace;
		var rockawayEntities = modelBuilder.Model
			.GetEntityTypes().Where(e =>
				e.ClrType.Namespace == rockawayEntityNamespace);
		foreach (var entity in rockawayEntities) {
			entity.SetTableName(entity.DisplayName());
		}

		modelBuilder.Entity<Artist>(entity => {
			entity.HasIndex(artist => artist.Slug).IsUnique();
			entity.HasMany(a => a.HeadlineShows)
				.WithOne(s => s.HeadlineArtist)
				.OnDelete(DeleteBehavior.Restrict);
			entity.HasMany(a => a.Endorsements)
				.WithMany(e => e.Artists)
				.UsingEntity(j => j.ToTable("Endorsement").HasData(SampleData.Endorsements));
		});

		modelBuilder.Entity<Venue>(entity => {
			entity.HasIndex(venue => venue.Slug).IsUnique();
			entity.HasMany(v => v.Shows)
				.WithOne(s => s.Venue)
				.OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<Show>(entity => {
			entity.HasKey(show => show.Venue.Id, show => show.Date);
			entity.HasMany(show => show.SupportSlots)
				.WithOne(ss => ss.Show).OnDelete(DeleteBehavior.Cascade);

			entity.HasMany(show => show.TicketTypes)
				.WithOne(tt => tt.Show).OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<TicketType>(entity => {
			entity.Property(tt => tt.Price).HasColumnType("money");
		});

		modelBuilder.Entity<Show>().HasKey(
			show => show.Venue.Id,
			show => show.Date
		);

		modelBuilder.Entity<SupportSlot>().HasKey(
			slot => slot.Show.Venue.Id,
			slot => slot.Show.Date,
			slot => slot.SlotNumber
		);

		modelBuilder.AddSampleData();
	}
}