using Microsoft.AspNetCore.Razor.TagHelpers;
using Rockaway.WebApp.Data;

namespace Rockaway.WebApp.TagHelpers;

public class LiTagHelper : TagHelper {
	public string? CountryFlag { get; set; } = default;

	public override void Process(TagHelperContext context, TagHelperOutput output) {
		if (String.IsNullOrEmpty(CountryFlag)) return;
		var country = Country.FromCode(CountryFlag);
		var html = $"<img src='/img/flags/1x/{country?.Code.ToLowerInvariant() ?? "unknown"}.png' />";
		output.Content.AppendHtml(html);
	}
}

public class CountryNameTagHelper : TagHelper {
	public string IsoCode { get; set; } = default!;
	public override void Process(TagHelperContext context, TagHelperOutput output) {
		output.TagName = "h4";
		output.TagMode = TagMode.StartTagAndEndTag;
		output.Content.Append(Country.FromCode(IsoCode)?.Name ?? $"Unknown country {IsoCode}");
	}
}



public class CountryFlagTagHelper : TagHelper {

	public string IsoCode { get; set; } = default!;

	private class FlagInfo {
		private readonly string src;
		private readonly string name;

		public FlagInfo(string code) {
			var country = Country.FromCode(code);
			src = (country?.Code ?? "unknown").ToLowerInvariant();
			name = country?.Name ?? $"unknown country {code}";
		}

		public string Src => $"/img/flags/1x/{src}.png";
		public string SrcSet => $"/img/flags/2x/{src}.png 2x,/img/flags/3x/{src}.png 3x";
		public string Alt => $"Flag of {name}";
		public string Title => name;
	}

	public override void Process(TagHelperContext _, TagHelperOutput output) {

		output.TagName = "img";
		output.TagMode = TagMode.SelfClosing;
		output.Attributes.Add("class", "country-flag");
		var info = new FlagInfo(IsoCode);

		output.Attributes.Add("src", info.Src);
		output.Attributes.Add("title", info.Title);
		output.Attributes.Add("alt", info.Alt);
		output.Attributes.Add("srcset", info.SrcSet);
	}
}