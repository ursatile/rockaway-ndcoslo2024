using Microsoft.AspNetCore.Mvc.RazorPages;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Pages;

public class ArtistModel(RockawayDbContext db) : PageModel {
	public Artist Artist = default!;

	public void OnGet(string slug) {
		var artist = db.Artists.FirstOrDefault(a => a.Slug == slug)!;
		// TODO: return not found
		// if (artist == default)
		Artist = artist;
	}
}