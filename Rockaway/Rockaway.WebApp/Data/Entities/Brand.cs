namespace Rockaway.WebApp.Data.Entities;

public class Brand {
	public Brand() { }

	public Brand(Guid id, string product) {
		Id = id;
		Product = product;
	}

	public Guid Id { get; set; }
	public string Product { get; set; } = String.Empty;
	public List<Artist> Artists { get; set; } = [];
}
