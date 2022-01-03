using System;
using System.Collections.Generic;

namespace softaware.Sitemap
{
    public class Sitemap
    {
        private readonly ICollection<SitemapNode> nodes = new HashSet<SitemapNode>();

        public void AddNode(SitemapNode node)
        {
            if (string.IsNullOrWhiteSpace(node.Url))
            {
                throw new ArgumentNullException($"{nameof(node.Url)} cannot be null");
            }

            this.nodes.Add(node);
        }

        public int Count => this.nodes.Count;

        public string GenerateSitemap()
        {
            throw new NotImplementedException();
        }
    }
}
