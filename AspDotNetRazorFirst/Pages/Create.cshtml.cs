using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspDotNetRazorFirst.wwwroot.entities;

namespace AspDotNetRazorFirst.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MovieController _context;

        public CreateModel(MovieController context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;
        
        


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Movies == null)
            {
                return Page();
            }

            int lastId = _context.Movies.Max(item => item.MovieId);
            Movie.MovieId = lastId + 1;
            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();

            WebScraper webScraper = new WebScraper("https://www.tf1.fr");

            return RedirectToPage("./Display");
        }
    }
}
