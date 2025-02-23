using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies_Tutorial_Application.Data;
using Movies_Tutorial_Application.Models;

namespace Movies_Tutorial_Application.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Movies_Tutorial_ApplicationContext _context; // Inject the DbContext

        public IndexModel(Movies_Tutorial_ApplicationContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            LatestMovie = await _context.Movie
            .OrderByDescending(movie => movie.Id)
            .FirstOrDefaultAsync();

            CheapestMovie = await _context.Movie
            .OrderBy(movie => movie.Price)
            .FirstOrDefaultAsync();


            ExpensiveMovie = await _context.Movie
            .OrderByDescending(movie => movie.Price)
            .FirstOrDefaultAsync();


        }

        public Movie? LatestMovie { get; set; }

        public Movie? CheapestMovie { get; set; }
        public Movie? ExpensiveMovie { get; set; }
      
        
    }
}
