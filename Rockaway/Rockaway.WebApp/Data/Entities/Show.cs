using System.Globalization;
using NodaTime;

namespace Rockaway.WebApp.Data.Entities;

public class Show {

	public Venue Venue { get; set; } = default!;

	public LocalDate Date { get; set; }

	public Artist HeadlineArtist { get; set; } = default!;

	public List<SupportSlot> SupportSlots { get; set; } = [];

	public int NextSupportSlotNumber
		=> (this.SupportSlots.Count > 0 ? this.SupportSlots.Max(s => s.SlotNumber) : 0) + 1;

	public List<TicketType> TicketTypes { get; set; } = [];

	public Dictionary<string, string> RouteData => new() {
		{ "venue", this.Venue.Slug },
		{ "date", this.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) }
	};
}

public class SupportSlot {
	public Show Show { get; set; } = default!;
	public int SlotNumber { get; set; }
	public Artist Artist { get; set; } = default!;
}