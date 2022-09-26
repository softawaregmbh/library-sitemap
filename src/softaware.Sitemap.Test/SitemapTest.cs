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
        public void SitemapWithAlternates_Test()
        {
            var sitemap = new Sitemap();
            sitemap.AddNode(new SitemapNode("https://www.example.com")
            {
                Alternates = new List<(string, string)>
                {
                    ("de", "https://www.example.com/de"),
                    ("fr", "https://www.example.com/fr")
                }
            });
            var result = sitemap.GenerateSitemap();

            var expected = File.ReadAllText("ExpectedResults/SitemapWithAlternates.xml");
            Assert.True(XNode.DeepEquals(XDocument.Parse(expected), XDocument.Parse(result)));
        }

        [Fact]
        public void SitemapWithBidirectionalAlternates_Test()
        {
            var sitemap = new Sitemap();
            sitemap.AddNodes(SitemapNode.FromLocalized(new List<LocalizedSitemapNode>
            {
                new LocalizedSitemapNode("en", new SitemapNode("https://www.example.com")),
                new LocalizedSitemapNode("de", new SitemapNode("https://www.example.com/de")),
            }));
            var result = sitemap.GenerateSitemap();

            var expected = File.ReadAllText("ExpectedResults/SitemapWithBidirectionalAlternates.xml");
            Assert.True(XNode.DeepEquals(XDocument.Parse(expected), XDocument.Parse(result)));
        }

        [Fact]
        public void SitemapWithMultipleAlternatesForOneLang_Test()
        {
            var sitemap = new Sitemap();
            sitemap.AddNodes(SitemapNode.FromLocalized(new List<LocalizedSitemapNode>
            {
                new LocalizedSitemapNode("de", "https://www.example.com/de1"),
                new LocalizedSitemapNode("de", "https://www.example.com/de2"),
            }));
            var result = sitemap.GenerateSitemap();

            var expected = File.ReadAllText("ExpectedResults/SitemapWithMultipleAlternatesForOneLang.xml");
            Assert.True(XNode.DeepEquals(XDocument.Parse(expected), XDocument.Parse(result)));
        }

        [Fact]
        public void SitemapWithAlternatesUsingFunc_Test()
        {
            var sitemap = new Sitemap();
            sitemap.AddNodes(SitemapNode.FromLocalized(
                new[] { "en", "de" }, 
                lang => new SitemapNode($"https://www.example.com/{lang}")));
            var result = sitemap.GenerateSitemap();

            var expected = File.ReadAllText("ExpectedResults/SitemapWithAlternatesUsingFunc.xml");
            Assert.True(XNode.DeepEquals(XDocument.Parse(expected), XDocument.Parse(result)));
        }
    }
}