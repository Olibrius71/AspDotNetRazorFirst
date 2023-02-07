using AngleSharp;
using AngleSharp.Dom;


namespace AspDotNetRazorFirst;

public class WebScraper
{
    private static IBrowsingContext _browsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
    private static IDocument document;
    
    private string SearchUrl { get; set; }

    public WebScraper(string searchUrl)
    {
        SearchUrl = searchUrl;
        GetHtml();
    }

    private async void GetHtml()
    {
        document = await _browsingContext.OpenAsync(SearchUrl);
        var cells = document.QuerySelectorAll(".Header_category_c4fHU");
        var titles = cells.Select(m => m.TextContent).ToList();
        foreach (var titre in titles)
        {
            Console.WriteLine(titre);
        }
    }

}