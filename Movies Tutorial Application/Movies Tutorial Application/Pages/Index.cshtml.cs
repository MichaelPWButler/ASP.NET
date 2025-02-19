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
            .OrderByDescending(m => m.Id)
            .FirstOrDefaultAsync();

            LatestTitle = LatestMovie?.Title;
            LatestGenre = LatestMovie?.Genre;
            LatestPrice = LatestMovie?.Price;
        }

        public Movie? LatestMovie { get; set; }
        public string? LatestTitle { get; set; }
        public string? LatestGenre { get; set; }
        public decimal? LatestPrice { get; set; }
    }
}
