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
        private readonly AspDotNetRazorFirst.MovieContext _context;

        public DisplayModel(AspDotNetRazorFirst.MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public int NbFilms { get; set; } = 0;
        public int NbSeries { get; set; } = 0;

        public async Task OnGetAsync()
        {
            if (_context.Movies != null)
            {
                Movie = await _context.Movies.ToListAsync();

                NbFilms = Movie.Count(m => m.MovieType==MovieType.Movie.ToString());
                NbSeries = Movie.Count(m => m.MovieType==MovieType.Series.ToString());
            }
        }

        
    }
}
