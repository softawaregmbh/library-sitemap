using System;

namespace softaware.Sitemap
{
    public class SitemapNode
    {
        public SitemapFrequency? Frequency { get; set; }

        public DateTime? LastModified { get; set; }

        public decimal? Priority { get; set; }

        public string Url { get; set; }
    }
}
