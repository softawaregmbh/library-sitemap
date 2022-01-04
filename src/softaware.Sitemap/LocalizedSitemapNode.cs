using System;

namespace softaware.Sitemap
{
    public sealed class LocalizedSitemapNode
    {
        public LocalizedSitemapNode(string language, SitemapNode node)
        {
            this.Language = language ?? throw new ArgumentNullException(nameof(language));
            this.Node = node ?? throw new ArgumentNullException(nameof(node));
        }

        public LocalizedSitemapNode(string language, string url)
            : this(language, new SitemapNode(url))
        {            
        }

        public string Language { get; set; }
        public SitemapNode Node { get; set; }
    }
}
