using Microsoft.Playwright;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class ScrapingService : IScrapingService
    {
        public async Task<string> GetFirstResultAsync(string query)
        {
            try
            {
                using var playwright = await Playwright.CreateAsync();

                // Chromium tarayıcısını başlat
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = true,
                    ExecutablePath = @"C:\Users\stjEnes.Curuktas\Chromium\chrome-win\chrome.exe"
                });

                var page = await browser.NewPageAsync();
                //await page.GotoAsync("https://ucelozelegitim.com/meb-ozelim-egitimdeyim-uygulamasi/");
                await page.GotoAsync("https://www.limasollunaci.com/");
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

                var content = await page.ContentAsync();
                await browser.CloseAsync();

                var paragraphs = ExtractContent(content, new[] { "p", "div" });
                var matchingSentences = FindMatchingSentences(paragraphs, query);

                //var result = new { Query = query, MatchingSentences = matchingSentences };
                var result = string.Join("\n", matchingSentences);
                return JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private List<string> ExtractContent(string htmlContent, string[] tags)
        {
            var contentList = new List<string>();
            foreach (var tag in tags)
            {
                var regex = new Regex($@"<{tag}\b[^>]*>(.*?)</{tag}>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                var matches = regex.Matches(htmlContent);

                foreach (Match match in matches)
                {
                    var content = match.Groups[1].Value.Trim();
                    contentList.Add(content);
                }
            }
            return contentList;
        }

        private List<string> FindMatchingSentences(List<string> paragraphs, string query)
        {
            var matchingSentences = new List<string>();
            foreach (var paragraph in paragraphs)
            {
                var sentences = Regex.Split(paragraph, @"(?<=[\.!\?])\s+");
                foreach (var sentence in sentences)
                {
                    if (sentence.Contains(query, StringComparison.OrdinalIgnoreCase))
                    {
                        matchingSentences.Add(sentence.Trim());
                    }
                }
            }
            return matchingSentences;
        }
    }
}
