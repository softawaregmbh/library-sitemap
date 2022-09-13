using System;
using System.Linq;
using System.Collections.Generic;

namespace softaware.Sitemap
{
    public sealed class SitemapNode
    {
        public SitemapNode(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException($"{nameof(url)} cannot be null");
            }

            this.Url = url;
        }

        public SitemapFrequency? Frequency { get; set; }
        public DateTime? LastModified { get; set; }
        public decimal? Priority { get; set; }
        public string Url { get; set; }
        public IEnumerable<(string Language, string Url)> Alternates { get; set; } = new List<(string, string)>();

        public static IEnumerable<SitemapNode> FromLocalized(IEnumerable<LocalizedSitemapNode> nodes)
        {
            var alternates = nodes.Select(n => (n.Language, n.Node.Url)).ToList();
            foreach (var node in nodes)
            {
                node.Node.Alternates = alternates;
            }

            return nodes.Select(n => n.Node).ToList();
        }

        public static IEnumerable<SitemapNode> FromLocalized(IEnumerable<string> languages, Func<string, SitemapNode> nodeSelector)
        {
            var urls = languages
                .Select(lang => new LocalizedSitemapNode(lang, nodeSelector(lang)))
                .ToList();

            return SitemapNode.FromLocalized(urls);
        }
    }
}
