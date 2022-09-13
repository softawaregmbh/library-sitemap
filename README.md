# Sitemap

## Example
``` csharp
using softaware.Sitemap;

var sitemap = new Sitemap();
sitemap.AddNode(new SitemapNode("https://www.example.com"));
var result = sitemap.GenerateSitemap();
```

Result:
``` xml
<urlset xmlns:xhtml="http://www.w3.org/1999/xhtml" 
		xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
	<url>
		<loc>https://www.example.com</loc>
	</url>
</urlset>
```

## Localized Sitemap
``` csharp
using softaware.Sitemap;

var sitemap = new Sitemap();

// Option 1) Specify alternates url:
sitemap.AddNode(new SitemapNode("https://www.example.com")
{
    Alternates = new List<(string, string)>
    {
        ("de", "https://www.example.com/de"),
        ("fr", "https://www.example.com/fr")
    }
});

// Option2 ) Using a callback function:
sitemap.AddNodes(SitemapNode.FromLocalized(
    new[] { "en", "de" }, 
    lang => new SitemapNode($"https://www.example.com/{lang}")));

var result = sitemap.GenerateSitemap();
```

Result:
``` xml
<urlset xmlns:xhtml="http://www.w3.org/1999/xhtml"
		xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
	<url>
		<loc>https://www.example.com</loc>
		<xhtml:link rel="alternate" hreflang="de" href="https://www.example.com/de" />
		<xhtml:link rel="alternate" hreflang="fr" href="https://www.example.com/fr" />
	</url>
</urlset>
```
