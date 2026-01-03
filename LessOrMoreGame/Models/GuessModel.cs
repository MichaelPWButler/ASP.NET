using LessOrMoreGame.Models.Enums;

namespace LessOrMoreGame.Models
{
    public class GuessModel
    {
        public bool IsCorrect { get; set; }
        public CountryModel NewCountry { get; set; }
        public CountryStat NewStat { get; set; }
    }
}