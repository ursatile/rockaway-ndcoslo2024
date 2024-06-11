// Rockaway.WebApp/Data/Entities/Sample/SampleData.Shows.cs

using Rockaway.WebApp.Data.Entities;
// ReSharper disable InconsistentNaming

namespace Rockaway.WebApp.Data.Sample;

public static partial class SampleData {

	public static Show WithTicketType(this Show show, Guid id, string name, decimal price, int? limit = null) {
		show.TicketTypes.Add(new(id, show, name, price, limit));
		return show;
	}

	public static Show WithSupportActs(this Show show, params Artist[] artists) {
		show.SupportSlots.AddRange(artists.Select(artist => new SupportSlot() {
			Show = show,
			Artist = artist,
			SlotNumber = show.NextSupportSlotNumber
		}));
		return show;
	}

	public static class Shows {

		private static int seed = 1;
		private static Guid NextId => TestGuid(seed++, 'C');

		public static readonly Show Coda_Barracuda_20240817 = Venues.Barracuda
			.BookShow(Artists.Coda, new(2024, 8, 17))
			.WithTicketType(NextId, "Upstairs unallocated seating", price: 25, limit: 100)
			.WithTicketType(NextId, "Downstairs standing", price: 25, limit: 120)
			.WithTicketType(NextId, "Cabaret table (4 people)", price: 120, limit: 10)
			.WithSupportActs(Artists.KillerBite, Artists.Overflow);

		public static readonly Show Coda_Columbia_20240818 = Venues.Columbia
			.BookShow(Artists.Coda, new(2024, 8, 18))
			.WithTicketType(NextId, "General Admission", price: 35)
			.WithTicketType(NextId, "VIP Meet & Greet", price: 75, limit: 20)
			.WithSupportActs(Artists.KillerBite, Artists.Overflow);

		public static readonly Show Coda_Bataclan_20240819 = Venues.Bataclan
			.BookShow(Artists.Coda, new(2024, 8, 19))
			.WithTicketType(NextId, "General Admission", price: 35)
			.WithTicketType(NextId, "VIP Meet & Greet", price: 75)
			.WithSupportActs(Artists.KillerBite, Artists.Overflow, Artists.JavasCrypt);

		public static readonly Show Coda_NewCrossInn_20240820 = Venues.NewCrossInn
			.BookShow(Artists.Coda, new(2024, 8, 20))
			.WithTicketType(NextId, "General Admission", price: 25)
			.WithTicketType(NextId, "VIP Meet & Greet", price: 55, limit: 20)
			.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_JohnDee_20240822 = Venues.JohnDee
			.BookShow(Artists.Coda, new(2024, 8, 22))
			.WithTicketType(NextId, "General Admission", price: 350)
			.WithTicketType(NextId, "VIP Meet & Greet", price: 750, limit: 25)
			.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_PubAnchor_20240823 = Venues.PubAnchor
			.BookShow(Artists.Coda, new(2024, 8, 23))
			.WithTicketType(NextId, "General Admission", price: 300)
			.WithTicketType(NextId, "VIP Meet & Greet", price: 720, limit: 10)
			.WithSupportActs(Artists.JavasCrypt);

		public static readonly Show Coda_Gagarin_20240825 =
			Venues.Gagarin.BookShow(Artists.Coda, new(2024, 8, 25))
			.WithTicketType(NextId, "General Admission", 25)
			.WithSupportActs(Artists.JavasCrypt, Artists.SilverMountainStringBand);

		public static readonly Show Linebreakers_Brewgata_20240614 =
			Venues.Brewgata.BookShow(Artists.Linebreakers, new(2024, 6, 14))
				.WithTicketType(NextId, "FREE", 0m);

		public static IEnumerable<Show> AllShows = [
			Coda_Barracuda_20240817,
			Coda_Columbia_20240818,
			Coda_Bataclan_20240819,
			Coda_NewCrossInn_20240820,
			Coda_JohnDee_20240822,
			Coda_PubAnchor_20240823,
			Coda_Gagarin_20240825,
			Linebreakers_Brewgata_20240614
		];

		public static IEnumerable<TicketType> AllTicketTypes
			=> AllShows.SelectMany(show => show.TicketTypes);

		public static IEnumerable<SupportSlot> AllSupportSlots
			=> AllShows.SelectMany(show => show.SupportSlots);
	}
}
