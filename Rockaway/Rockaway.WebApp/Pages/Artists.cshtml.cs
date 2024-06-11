using Microsoft.AspNetCore.Mvc.RazorPages;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Models;

namespace Rockaway.WebApp.Pages;

public class ArtistsModel(RockawayDbContext db) : PageModel {
	public IEnumerable<ArtistViewData> Artists = default!;

	public void OnGet() {
		Artists = db.Artists
			.OrderBy(a => a.Name)
			.Select(a => new ArtistViewData(a));
	}
}