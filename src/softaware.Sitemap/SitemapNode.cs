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
        public IDictionary<string, string> Alternatives { get; set; } = new Dictionary<string, string>();

        public static IEnumerable<SitemapNode> FromLocalized(IDictionary<string, SitemapNode> nodes)
        {
            foreach (var node in nodes)
            {
                node.Value.Alternatives = nodes
                    .Where(n => n.Key != node.Key)
                    .ToDictionary(n => n.Key, n => n.Value.Url);
            }

            return nodes.Select(n => n.Value).ToList();
        }

        public static IEnumerable<SitemapNode> FromLocalized(IEnumerable<string> languages, Func<string, string> urlSelector)
        {
            var urls = languages.ToDictionary(
                lang => lang,
                lang => new SitemapNode(urlSelector(lang)));

            return SitemapNode.FromLocalized(urls);
        }
    }
}
