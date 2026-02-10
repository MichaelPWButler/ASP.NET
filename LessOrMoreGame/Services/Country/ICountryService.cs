using LessOrMoreGame.Models;

namespace LessOrMoreGame.Services.Country
{
    public interface ICountryService
    {
        StartGameModel StartGame();
        GuessModel Guess(CheckAnswerModel checkAnswerModel);
    }
}