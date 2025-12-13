using LessOrMoreGame.Models;
using LessOrMoreGame.wwwroot.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LessOrMoreGame.Pages
{
   // [IgnoreAntiforgeryToken(Order = 10001)]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly JsonFileCountryService _JsonFileCountryService;
        private List<Country> AllCountries = [];

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileCountryService jsonFileCountryService)
        {
            _logger = logger;
            _JsonFileCountryService = jsonFileCountryService;
        }

        public IActionResult OnPostCheckCard([FromBody] int request)
        {
            return new JsonResult(false);
        }

        public void OnGet()
        {
            Random _Random = new Random();
            AllCountries = _JsonFileCountryService.GetCountries().ToList();

            List<Country> _Country = AllCountries.OrderBy(country => _Random.Next()).Take(2).ToList();
            Country1 = _Country[0];
            Country2 = _Country[1];

        }

        public Country Country1 { get; set; }
        public Country Country2 { get; set; }
    }
}
