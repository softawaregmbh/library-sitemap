using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Xunit;

namespace softaware.Sitemap.Test
{
    public class SitemapTest
    {
        [Fact]
        public void SimpleSitemap_Test()
        {
            var sitemap = new Sitemap();
            sitemap.AddNode(new SitemapNode("https://www.example.com"));
            var result = sitemap.GenerateSitemap();

            var expected = File.ReadAllText("ExpectedResults/SimpleSitemap.xml");
            Assert.True(XNode.DeepEquals(XDocument.Parse(expected), XDocument.Parse(result)));
        }

        [Fact]
        public void SitemapWithAlternatives_Test()
        {
            var sitemap = new Sitemap();
            sitemap.AddNode(new SitemapNode("https://www.example.com")
            {
                Alternatives = new Dictionary<string, string>
                {
                    ["de"] = "https://www.example.com/de",
                    ["fr"] = "https://www.example.com/fr"
                }
            });
            var result = sitemap.GenerateSitemap();

            var expected = File.ReadAllText("ExpectedResults/SitemapWithAlternatives.xml");
            Assert.True(XNode.DeepEquals(XDocument.Parse(expected), XDocument.Parse(result)));
        }

        [Fact]
        public void SitemapWithBidirectionalAlternatives_Test()
        {
            var sitemap = new Sitemap();
            sitemap.AddNodes(SitemapNode.FromLocalized(new Dictionary<string, SitemapNode>
            {
                ["en"] = new SitemapNode("https://www.example.com"),
                ["de"] = new SitemapNode("https://www.example.com/de"),
            }));
            var result = sitemap.GenerateSitemap();

            var expected = File.ReadAllText("ExpectedResults/SitemapWithBidirectionalAlternatives.xml");
            Assert.True(XNode.DeepEquals(XDocument.Parse(expected), XDocument.Parse(result)));
        }
    }
}