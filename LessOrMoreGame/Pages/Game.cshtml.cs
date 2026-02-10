using LessOrMoreGame.Models;
using LessOrMoreGame.Models.Enums;
using LessOrMoreGame.Services.Country;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace LessOrMoreGame.Pages
{
    public class GameModel : PageModel
    {
        private readonly ILogger<GameModel> _logger;
        private readonly ICountryService _CountryService;

        public GameModel(ILogger<GameModel> logger,
            ICountryService countryService)
        {
            _logger = logger;
            _CountryService = countryService;
        }

        public IActionResult OnPostCheckCard([FromBody] CheckAnswerModel request)
        {
            GuessModel _CheckAnswer = _CountryService.Guess(request);

            NumberOfLives = !_CheckAnswer.IsCorrect ? NumberOfLives - 1 : NumberOfLives;

            return new JsonResult(new 
            { 
                IsCorrect = _CheckAnswer.IsCorrect,
                NewCountry = _CheckAnswer.NewCountry,
                NewQuestion = _CheckAnswer.NewStat.ToQuestionText(),
                NewStat = _CheckAnswer.NewStat,
                NewLives = NumberOfLives
            });
        }

        public async Task<IActionResult> OnGetAsync()
        {
            StartGameModel _GameData = _CountryService.StartGame();
            Country1 = _GameData.Country1;
            Country2 = _GameData.Country2;
            Stat = _GameData.CountryStat;
            QuestionText = Stat.ToQuestionText();

            return Page();
        }

        public CountryModel Country1 { get; private set; }
        public CountryModel Country2 { get; private set; }
        public string QuestionText { get; private set; }
        public CountryStat Stat { get; private set; }
        public int NumberOfLives
        {
            get => HttpContext.Session.GetInt32("NumberOfLives") ?? 3;
            set => HttpContext.Session.SetInt32("NumberOfLives", value);
        }
    }
}