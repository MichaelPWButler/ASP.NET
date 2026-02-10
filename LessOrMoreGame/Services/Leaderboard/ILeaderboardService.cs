using LessOrMoreGame.Models;

namespace LessOrMoreGame.Services.Leaderboard
{
    public interface ILeaderboardService
    {
        IEnumerable<LeaderboardModel> GetLeaderboard();
    }
}