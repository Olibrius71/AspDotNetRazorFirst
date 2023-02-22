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

        public IActionResult OnGetAsync(string movieName)
        {
            Task<string> link = GetLinkToScrap(movieName);
            return Page();
        }

        private async Task<string> GetLinkToScrap(string movieName)
        {
            WebScraper webScraper = new WebScraper(Movie.MovieName);
            await webScraper.GetHtml();
            string link = webScraper.ToString();
            return link;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        private IList<Movie>? NewMovies { get; set; } = null;
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (NewMovies is null)
            {
                
            }
            else
            {
                
            }
                
            if (!ModelState.IsValid || _context.Movies == null || Movie == null)
            {
                return Page();
            }

            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
