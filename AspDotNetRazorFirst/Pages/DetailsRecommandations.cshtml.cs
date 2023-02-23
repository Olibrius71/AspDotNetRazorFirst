using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspDotNetRazorFirst;
using AspDotNetRazorFirst.wwwroot.entities;

namespace AspDotNetRazorFirst.Pages
{
    public class DetailsRecommandationsModel : PageModel
    {
        private readonly MovieContext _context;

        public DetailsRecommandationsModel(MovieContext context)
        {
            _context = context;
        }
        
        
        [BindProperty]
        private IList<Movie>? NewMovies { get; set; } = null;

        public async Task<IActionResult> OnGetAsync(string movieName)
        {
            WebScraper getNeededLinkWebScraper = new WebScraper(movieName);
            await getNeededLinkWebScraper.GetHtml();
            string moviePagelink = getNeededLinkWebScraper.GetFullMoviePageLink();

            WebScraper webScraper = new WebScraper(movieName, true, moviePagelink);
            await webScraper.GetHtml();
            string newMovieName = webScraper.GetNewMoviesTitles()[0];
            
            return Page();
        }

        

        public async Task<IActionResult> OnPostAsync()
        {
            if (NewMovies is null)
            {
                
            }
            else
            {
                
            }
                
            if (!ModelState.IsValid || _context.Movies == null || NewMovies == null)
            {
                return Page();
            }

            _context.Movies.Add(NewMovies[0]);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
