using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace CheckLinksConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var site = "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps";
            var client = new HttpClient();
            var body = client.GetStringAsync(site);

            Console.WriteLine(body.Result);

            Console.WriteLine();
            Console.WriteLine("Links:");
            var links = LinkChecker.GetLinks(body.Result);
            links.ToList().ForEach(Console.WriteLine);
        }
    }

    internal class LinkChecker
    {
        public static IEnumerable<string> GetLinks(string page)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(page);
            var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]")
                .Select(n => n.GetAttributeValue("href", string.Empty))
                .Where(l => !string.IsNullOrEmpty(l))
                .Where(l => l.StartsWith("http"));
            return links;
        }
    }
}
