using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspDotNetRazorFirst.wwwroot.entities;


namespace AspDotNetRazorFirst.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MovieContext _context;

        public CreateModel(MovieContext context)
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
            
            MovieDbWebScraper webScraper = new MovieDbWebScraper(Movie.MovieName);
            await webScraper.GetHtml();
            
            string finalMovieName = webScraper.GetTitle();
            string movieDate = webScraper.GetDate();
            string movieDesc = webScraper.GetDescription();
            byte[] movieImageData = await webScraper.GetImage();
            string movieType = webScraper.GetType();
            
            Movie.MovieName = finalMovieName;
            if (movieDate[0] == ' ')  // If the number of the day is below 10, it will throw an error without this
            {
                movieDate = "0" + movieDate.Substring(1);
            }
            Movie.MovieDate = DateTime.ParseExact(movieDate,"dd MMMM yyyy",CultureInfo.CurrentCulture);
            Movie.MovieDesc = movieDesc;
            Movie.MovieImageData = movieImageData;
            Movie.MovieType = movieType;
            
            
            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Display");
        }
    }
}
