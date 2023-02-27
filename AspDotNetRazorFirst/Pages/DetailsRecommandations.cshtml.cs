using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspDotNetRazorFirst;
using AspDotNetRazorFirst.wwwroot.entities;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace AspDotNetRazorFirst.Pages
{
    public class DetailsRecommandationsModel : PageModel
    {
        private readonly MovieContext _context;

        public DetailsRecommandationsModel(MovieContext context)
        {
            _context = context;
        }

        private static int NumberMoviesToRecommend = 6;
       
        [BindProperty]
        public IList<Movie>? NewMovies { get; set; } = new List<Movie>();
        
        
        public async Task<IActionResult> OnGetAsync(string movieName, string movieType)
        {
            TasteDiveWebScraper recommandationsWebScraper = new TasteDiveWebScraper(movieName, movieType);
            await recommandationsWebScraper.GetHtml();
            recommandationsWebScraper.InitializeCards();
            
            List<string> newMoviesTitle = recommandationsWebScraper.GetNewTitles();
            List<string> newMoviesDates = recommandationsWebScraper.GetNewDates();
            List<string> newMoviesDescs = recommandationsWebScraper.GetNewDescs();

            for (int movieIndex = 0; movieIndex < NumberMoviesToRecommend; movieIndex++)
            {
                Movie movieToAdd = new Movie();
                movieToAdd.MovieName = newMoviesTitle[movieIndex];
                movieToAdd.MovieDate = DateTime.ParseExact(newMoviesDates[movieIndex],"yyyy",CultureInfo.CurrentCulture);
                movieToAdd.MovieDesc = newMoviesDescs[movieIndex];
                movieToAdd.MovieType = movieType;
                movieToAdd.MovieImageData = await recommandationsWebScraper.GetNewImageWithIndex(movieIndex); 
                NewMovies.Add(movieToAdd);
            }

            var jsonNewMovies = JsonConvert.SerializeObject(NewMovies);
            
            HttpContext.Session.SetString("NewMovies", jsonNewMovies);

            return Page();
        }

        

        public async Task<IActionResult> OnPostAsync()
        {
            string jsonNewMovies = HttpContext.Session.GetString("NewMovies");
            
            IList<Movie> NewMovies = JsonConvert.DeserializeObject<List<Movie>>(jsonNewMovies);
            
       
            var selectedMovieIndexes = Request.Form["newMoviesToAdd"];

            IList<Movie> moviesToAdd = new List<Movie>();
            foreach (var newMovie in selectedMovieIndexes)
            {
                moviesToAdd.Add(NewMovies[Int32.Parse(newMovie)]);    
            }
            
            _context.Movies.AddRange(moviesToAdd);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Display");
        }
    }
}
