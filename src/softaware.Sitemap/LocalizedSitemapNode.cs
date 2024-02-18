using System;

namespace softaware.Sitemap
{
    public sealed class LocalizedSitemapNode(string language, SitemapNode node)
    {
        public LocalizedSitemapNode(string language, string url)
            : this(language, new SitemapNode(url))
        {            
        }

        public string Language { get; set; } = language ?? throw new ArgumentNullException(nameof(language));
        public SitemapNode Node { get; set; } = node ?? throw new ArgumentNullException(nameof(node));
    }
}
