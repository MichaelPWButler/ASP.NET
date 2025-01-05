using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movies_Tutorial_Application.Data;
using Movies_Tutorial_Application.Models;

namespace Movies_Tutorial_Application.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly Movies_Tutorial_Application.Data.Movies_Tutorial_ApplicationContext _context;

        public IndexModel(Movies_Tutorial_Application.Data.Movies_Tutorial_ApplicationContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
