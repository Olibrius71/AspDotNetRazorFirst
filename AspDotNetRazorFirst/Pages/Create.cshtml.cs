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
    public class CreateModel : PageModel
    {
        private readonly AspDotNetRazorFirst.MovieController _context;

        public CreateModel(AspDotNetRazorFirst.MovieController context)
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
          if (!ModelState.IsValid || _context.Movies == null || Movie == null)
            {
                return Page();
            }

            int lastId = _context.Movies.Max(item => item.MovieId);
            Movie.MovieId = lastId + 1;
            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Display");
        }
    }
}
