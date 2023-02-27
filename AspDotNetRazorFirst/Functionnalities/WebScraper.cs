using AngleSharp;
using AngleSharp.Dom;

namespace AspDotNetRazorFirst;

public abstract class WebScraper
{
    protected static IBrowsingContext _browsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
    
    protected static IDocument document;

    protected static HttpClient _httpClient = new HttpClient();
    
    protected string SearchUrl { get; set; }
    
    public async Task GetHtml()
    {
        document = await _browsingContext.OpenAsync(SearchUrl);
    }
}