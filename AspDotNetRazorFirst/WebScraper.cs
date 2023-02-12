using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom;


namespace AspDotNetRazorFirst;

public class WebScraper
{
    private static IBrowsingContext _browsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
    private static IDocument document;
    
    private string SearchUrl { get; set; } = "https://www.themoviedb.org/search?language=fr&query=";

    public WebScraper(string movieName)
    {
        string[] movieWords = movieName.Split(" ");
        foreach (var word in movieWords)
        {
            string finalWord = Regex.Replace(word, @"[#&,$¤£;:]", "");
            SearchUrl += finalWord;
            if (Array.IndexOf(movieWords,word) != movieWords.Length - 1)
            {
                SearchUrl += "+";
            }
        }
    }

    public async Task GetHtml()
    {
        document = await _browsingContext.OpenAsync(SearchUrl);
    }

    public string GetDate()
    { 
        var cells = document.QuerySelectorAll(".results.flex > .card .release_date");
        string date = cells.Select(m => m.TextContent).ToList().First();

        return date;
    }

    public string GetTitle()
    {
        var cells = document.QuerySelectorAll(".results.flex > .card > .wrapper > .details > .wrapper > .title > div > a.result > h2");
        string title = cells.Select(m => m.TextContent).ToList().First();

        return title;
    }

    public string GetDescription()
    {
        var cells = document.QuerySelectorAll(".results.flex > .card > .wrapper > .details > .overview > p");
        string description = cells.Select(m => m.TextContent).ToList().First();

        return description;
    }

}