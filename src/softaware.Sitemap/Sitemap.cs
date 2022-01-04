using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace softaware.Sitemap
{
    public sealed class Sitemap
    {
        private readonly List<SitemapNode> nodes = new List<SitemapNode>();

        public void AddNode(SitemapNode node)
            => this.nodes.Add(node);

        public void AddNodes(IEnumerable<SitemapNode> nodes)
            => this.nodes.AddRange(nodes);

        public int Count => this.nodes.Count;

        public string GenerateSitemap()
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace xhtml = "http://www.w3.org/1999/xhtml";

            XElement root = new XElement(
                xmlns + "urlset",
                new XAttribute(XNamespace.Xmlns + "xhtml", xhtml));

            foreach (SitemapNode node in nodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(node.Url)),
                    node.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        node.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    node.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        node.Frequency.Value.ToString().ToLowerInvariant()),
                    node.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        node.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)),
                    node.Alternatives?.Select(alt => new XElement(
                        xhtml + "link",
                        new XAttribute("rel", "alternative"),
                        new XAttribute("hreflang", alt.Language),
                        new XAttribute("href", alt.Url)
                    )));

                root.Add(urlElement);
            }

            var document = new XDocument(root);
            return document.ToString();
        }
    }
}
