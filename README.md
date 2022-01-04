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

