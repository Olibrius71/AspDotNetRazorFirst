using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspDotNetRazorFirst;
using AspDotNetRazorFirst.wwwroot.entities;


namespace AspDotNetRazorFirst.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly MovieContext _context;

        public DetailsModel(MovieContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }
            else 
            {
                Movie = movie;
            }
            return Page();
        }
    }
    
}
