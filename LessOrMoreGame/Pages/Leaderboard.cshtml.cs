using LessOrMoreGame.Services.Leaderboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NsModel = LessOrMoreGame.Models;

namespace LessOrMoreGame.Pages
{
    public class LeaderboardModel : PageModel
    {
        private readonly ILeaderboardService _LeaderboardService;
        public List<NsModel.LeaderboardModel> _Leaderboard;

        public LeaderboardModel(ILeaderboardService leaderboardService)
        {
            _LeaderboardService = leaderboardService;
        }

        public void OnGet()
        {
            _Leaderboard = _LeaderboardService.GetLeaderboard().ToList();
        }
    }
}
