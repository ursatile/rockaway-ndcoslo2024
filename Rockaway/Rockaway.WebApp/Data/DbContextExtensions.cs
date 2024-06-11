using Microsoft.AspNetCore.Identity;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

public static class DbContextExtensions {
	public static void AddSampleData(this ModelBuilder modelBuilder) {

		modelBuilder.Entity<Artist>()
			.HasData(SeedData.For(SampleData.Artists.AllArtists));

		modelBuilder.Entity<Venue>()
			.HasData(SeedData.For(SampleData.Venues.AllVenues));

		modelBuilder.Entity<Show>()
			.HasData(SeedData.For(SampleData.Shows.AllShows));

		modelBuilder.Entity<SupportSlot>()
			.HasData(SeedData.For(SampleData.Shows.AllSupportSlots));
		modelBuilder.Entity<Brand>()
			.HasData(SeedData.For(SampleData.Brands.AllBrands));

		modelBuilder.Entity<IdentityUser>().HasData(SampleData.Users.Admin);
	}
}
