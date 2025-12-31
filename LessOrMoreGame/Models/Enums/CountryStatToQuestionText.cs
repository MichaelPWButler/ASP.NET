using System.Runtime.CompilerServices;

namespace LessOrMoreGame.Models.Enums
{
    internal static class CountryStatToQuestionText
    {
        internal static string ToQuestionText(this CountryStats stat)
        {
            switch (stat)
            {
                case CountryStats.Population:
                    return "Which Country Has The Highest Population?";
                case CountryStats.LandArea:
                    return "Which Country Has The Largest Land Area?";
                default:
                    return "Just pick one :)";
            }
        }
    }
}
