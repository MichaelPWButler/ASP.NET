using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies_Tutorial_Application.Models;

namespace Movies_Tutorial_Application.Data
{
    public class Movies_Tutorial_ApplicationContext : DbContext
    {
        public Movies_Tutorial_ApplicationContext (DbContextOptions<Movies_Tutorial_ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Movies_Tutorial_Application.Models.Movie> Movie { get; set; } = default!;
    }
}
