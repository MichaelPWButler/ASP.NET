using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            // <snippet_search_linqQuery>
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;
            // </snippet_search_linqQuery>

            var movies = from m in _context.Movie
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }

            // <snippet_search_selectList>
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            // </snippet_search_selectList>

            Movie = await movies.ToListAsync();
        
        }
    }
}
