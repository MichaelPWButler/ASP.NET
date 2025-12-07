using LessOrMoreGame.Models;
using LessOrMoreGame.wwwroot.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LessOrMoreGame.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly JsonFileCountryService _JsonFileCountryService;

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileCountryService jsonFileCountryService)
        {
            _logger = logger;
            _JsonFileCountryService = jsonFileCountryService;
        }

        public void OnGet()
        {
            Countries = _JsonFileCountryService.GetCountries();
        }

        public IEnumerable<Country> Countries { get; private set; } = [];
    }
}
