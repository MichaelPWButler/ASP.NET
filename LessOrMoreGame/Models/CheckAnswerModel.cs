using LessOrMoreGame.Models.Enums;

namespace LessOrMoreGame.Models
{
    public class CheckAnswerModel
    {
        public int CountrySelectedID { get; set; }
        public int OtherCountryID { get; set; }
        public CountryStat Stat { get; set; }
    }
}
