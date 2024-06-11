using Rockaway.WebApp.Data.Entities;
// ReSharper disable InconsistentNaming

namespace Rockaway.WebApp.Data.Sample;

public static partial class SampleData {

	public static Show WithSupportActs(this Show show, params Artist[] artists) {
		show.SupportSlots.AddRange(artists.Select(artist => new SupportSlot() {
			Show = show,
			Artist = artist,
			SlotNumber = show.NextSupportSlotNumber
		}));
		return show;
	}
	public static class Shows {
		public static readonly Show Coda_Barracuda_20240817 = Venues.Barracuda
			.BookShow(Artists.Coda, new(2024, 8, 17))
			.WithSupportActs(Artists.KillerBite, Artists.Overflow);

		public static readonly Show Coda_Columbia_20240818 = Venues.Columbia
			.BookShow(Artists.Coda, new(2024, 8, 18))
			.WithSupportActs(Artists.KillerBite, Artists.Overflow);

		public static readonly Show Coda_Bataclan_20240819 = Venues.Bataclan
			.BookShow(Artists.Coda, new(2024, 8, 19))
			.WithSupportActs(Artists.KillerBite, Artists.Overflow, Artists.JavasCrypt);


		public static readonly Show Coda_NewCrossInn_20240820 = Venues.NewCrossInn
			.BookShow(Artists.Coda, new(2024, 8, 20))
			.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_JohnDee_20240822 = Venues.JohnDee
			.BookShow(Artists.Coda, new(2024, 8, 22))
			.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_PubAnchor_20240823 = Venues.PubAnchor
			.BookShow(Artists.Coda, new(2024, 8, 23))
			.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_Gagarin_20240825 =
			Venues.Gagarin.BookShow(Artists.Coda, new(2024, 8, 25))
			.WithSupportActs(Artists.JavasCrypt, Artists.SilverMountainStringBand);

		public static IEnumerable<Show> AllShows = [
			Coda_Barracuda_20240817,
			Coda_Columbia_20240818,
			Coda_Bataclan_20240819,
			Coda_NewCrossInn_20240820,
			Coda_JohnDee_20240822,
			Coda_PubAnchor_20240823,
			Coda_Gagarin_20240825
		];

		public static IEnumerable<SupportSlot> AllSupportSlots
			=> AllShows.SelectMany(show => show.SupportSlots);
	}
}