using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspDotNetRazorFirst.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    
    
    public int? NbFilms { get; set; }
    

    public void OnGet()
    {
        if (NbFilms is null)
        {
            NbFilms = 0;
        }
    }
}