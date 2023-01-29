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
    public class DisplayModel : PageModel
    {
        private readonly AspDotNetRazorFirst.MovieController _context;

        public DisplayModel(AspDotNetRazorFirst.MovieController context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Movies != null)
            {
                Movie = await _context.Movies.ToListAsync();
            }
        }
    }
}
