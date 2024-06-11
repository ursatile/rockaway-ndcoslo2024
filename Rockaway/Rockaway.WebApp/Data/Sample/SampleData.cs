using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Data.Sample;

public static partial class SampleData {

	private static Guid TestGuid(int seed, char pad) => new(seed.ToString().PadLeft(32, pad));

	public static object[] Endorsements = new[] {
		new { ArtistsId = Artists.AlterColumn.Id, EndorsementsId = Brands.Nike.Id },
		new { ArtistsId = Artists.AlterColumn.Id, EndorsementsId = Brands.Pingvin.Id },
		new { ArtistsId = Artists.AlterColumn.Id, EndorsementsId = Brands.PepsiCola.Id },
		new { ArtistsId = Artists.BodyBag.Id, EndorsementsId = Brands.Nike.Id },
		new { ArtistsId = Artists.Coda.Id, EndorsementsId = Brands.Pingvin.Id },
		new { ArtistsId = Artists.DevLeppard.Id, EndorsementsId = Brands.PepsiCola.Id }
	};
}