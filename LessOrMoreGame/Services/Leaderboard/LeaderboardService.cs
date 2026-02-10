using LessOrMoreGame.Models;
using System.Text.Json;

namespace LessOrMoreGame.Services.Leaderboard
{
    public class LeaderboardService : ILeaderboardService
    {
        private IWebHostEnvironment WebHostEnvironment { get; }

        public LeaderboardService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data", "Leaderboard.json"); }
        }

        public IEnumerable<LeaderboardModel> GetLeaderboard()
        {
            StreamReader _Reader = File.OpenText(JsonFileName);
            IEnumerable<LeaderboardModel>? _Leaderboard = new List<LeaderboardModel>();

            _Leaderboard = JsonSerializer.Deserialize<LeaderboardModel[]>(_Reader.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            }).OrderBy(leaderboard => leaderboard.Score);

            return _Leaderboard ?? new List<LeaderboardModel>();
        }
    }
}
