using LessOrMoreGame.Models;
using LessOrMoreGame.wwwroot.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace LessOrMoreGame.Pages
{
   // [IgnoreAntiforgeryToken(Order = 10001)]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICountryService _CountryService;

        public IndexModel(ILogger<IndexModel> logger,
            ICountryService countryService)
        {
            _logger = logger;
            _CountryService = countryService;
        }

        public IActionResult OnPostCheckCard([FromBody] GuessModel request)
        {
            return new JsonResult(false);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            StartGameModel _GameData = _CountryService.StartGame();
            Country1 = _GameData.Country1;
            Country2 = _GameData.Country2;

            return Page();
        }

        public Country Country1 { get; set; }
        public Country Country2 { get; set; }
    }
}
