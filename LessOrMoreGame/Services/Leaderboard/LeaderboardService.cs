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
            IEnumerable<LeaderboardModel>? _Leaderboard = new List<LeaderboardModel>();

            _Leaderboard = JsonSerializer.Deserialize<LeaderboardModel[]>(File.ReadAllText(JsonFileName),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            }).OrderByDescending(leaderboard => leaderboard.Score);

            return _Leaderboard ?? new List<LeaderboardModel>();
        }

        public void AddToLeaderBoard(LeaderboardModel model)
        {
            IEnumerable<LeaderboardModel>? _Leaderboard = new List<LeaderboardModel>();

            _Leaderboard = JsonSerializer.Deserialize<LeaderboardModel[]>(File.ReadAllText(JsonFileName),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            _Leaderboard = _Leaderboard.Append(model);

            string _Update = JsonSerializer.Serialize(_Leaderboard,
            new JsonSerializerOptions
            {
                WriteIndented = true 
            });

            File.WriteAllText(JsonFileName, _Update);
        }
    }
}
