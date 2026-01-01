using System.Runtime.CompilerServices;

namespace LessOrMoreGame.Models.Enums
{
    internal static class CountryStatToQuestionText
    {
        internal static string ToQuestionText(this CountryStat stat)
        {
            switch (stat)
            {
                case CountryStat.Population:
                    return "Which Country Has The Highest Population?";
                case CountryStat.LandArea:
                    return "Which Country Has The Largest Land Area?";
                default:
                    return "Just pick one :)";
            }
        }
    }
}
