using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspDotNetRazorFirst;
using AspDotNetRazorFirst.wwwroot.entities;
using AspDotNetRazorFirst.wwwroot.enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspDotNetRazorFirst.Pages
{
    public class DisplayModel : PageModel
    {
        private readonly MovieContext _context;

        public DisplayModel(MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public int NbFilms { get; set; } = 0;
        public int NbSeries { get; set; } = 0;

        public string? DisplayMode { get; set; } = null;

        public async Task OnGetAsync(string? displayMode)
        {
            DisplayMode = displayMode;
            
            if (_context.Movies != null)
            {
                Movie = await _context.Movies.ToListAsync();

                NbFilms = Movie.Count(m => m.MovieType == MovieType.Movie.ToString());
                NbSeries = Movie.Count(m => m.MovieType == MovieType.Series.ToString());
                
                if (DisplayMode == MovieType.Movie.ToString())
                {
                    Movie = Movie.Where(movie => movie.MovieType == MovieType.Movie.ToString()).ToList();
                }
                else if (DisplayMode == MovieType.Series.ToString())
                {
                    Movie = Movie.Where(movie => movie.MovieType == MovieType.Series.ToString()).ToList();
                }
            }
        }
        /*
        public async Task OnPost()
        {
            if (_context.Movies != null)
            {
                Movie = await _context.Movies.Where(movie => movie.MovieType == MovieType.Series.ToString()).ToListAsync();

                NbFilms = Movie.Count(m => m.MovieType==MovieType.Movie.ToString());
                NbSeries = Movie.Count(m => m.MovieType==MovieType.Series.ToString());
            }
        }

        */
    }
}
