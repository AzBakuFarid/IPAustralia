using HtmlAgilityPack;
using IPAustralia.Domain;
using IPAustralia.Extensions;
using IPAustralia.Models;
using IPAustralia.ServiceAbstractions;
using System.Web;

namespace IPAustralia.Services
{
    public class Scrapper : IScrapper
    {
        public List<Trademark> ScrapForTrademarks(TrademarkScrapData data)
        {
            var result = new List<Trademark>();
            if (data is not null) 
            {
                HtmlDocument page = new();
                page.LoadHtml(data.Html);
                var rows = page.DocumentNode.SelectNodes("//table[@id='resultsTable']//tbody");
                result = rows.EmptyIfNull().Select(s => ProcessRow(s, data.DomainUrl)).ToList();
            }
            return result;
        }

        public string FindCsrf(string html)
        {
            string result = null;
            if (html is not null)
            {
                HtmlDocument page = new();
                page.LoadHtml(html);
                result = page.DocumentNode.SelectNodes("//form[@id='basicSearchForm']//input")
                    .FirstOrDefault(f => f.GetAttributeValue("name", null) == "_csrf")?
                    .GetAttributeValue("value", null);
            }
            return result;
        }

        private Trademark ProcessRow(HtmlNode node, string domainUrl)
        {
            var statuses = PrepareStatuses(node.SelectSingleNode(".//td[@class='status']"));

            return new Trademark
            {
                Number = PrepareNumber(node.SelectSingleNode(".//td[@class='number']")),
                LogoUrl = PrepareLogo(node.SelectSingleNode(".//td[@class='trademark image']//img")),
                Name = PrepareName(node.SelectSingleNode(".//td[@class='trademark words']")),
                Classes = PrepareClasses(node.SelectSingleNode(".//td[@class='classes ']")),
                DetailsUrl = PrepareDetailsUrl(node.SelectSingleNode(".//td[@class='number']//a"), domainUrl),
                Status1 = statuses.FirstOrDefault(),
                Status2 = statuses.Skip(1).FirstOrDefault(),
            };

        }

        private string PrepareNumber(HtmlNode node) =>
            node?.InnerText?.Replace("\n", " ").Trim();
        private string PrepareLogo(HtmlNode node) =>
            node?.GetAttributeValue("src", null)?.Replace("\n", " ").Trim();
        private string PrepareName(HtmlNode node)
        {
            var name = node.InnerText?.Replace("\n", " ").Trim();
            return name.IsMissing() ? name : HttpUtility.HtmlDecode(name);
        }
        private string PrepareClasses(HtmlNode node) =>
            node?.InnerText?.Replace("\n", " ").Trim();
        private IEnumerable<string> PrepareStatuses(HtmlNode node) =>
            node?.InnerText.Split(":").EmptyIfNull().Select(s => s.Replace("\n", " ").Trim().Replace("&#9679;", ""));
        private string PrepareDetailsUrl(HtmlNode node, string domainUrl)
        {
            string value = null;
            if (node is not null)
            {
                var hrefAttr = node.GetAttributeValue("href", null);
                var hrefAttrValue = hrefAttr?.Split("?").FirstOrDefault()?.Replace("\n", "");
                value = $"{domainUrl}{hrefAttrValue}";
            }
            return value;
        }

        public string FindSearchId(string html)
        {
            string result = null;
            if (html is not null)
            {
                HtmlDocument page = new();
                page.LoadHtml(html);
                result =  page.DocumentNode.SelectNodes("//div[@id='refineResults']//form//input")
                    .FirstOrDefault(f => f.GetAttributeValue("name", null) == "s")?
                    .GetAttributeValue("value", null);
            }
            return result;
        }
    }
}
