using System.Text.Json;

namespace LessOrMoreGame.Models
{
    public class LeaderboardModel
    {
        public int Score { get; set; }
        public string? Name { get; set; }
        public override string ToString() => JsonSerializer.Serialize<LeaderboardModel>(this);
    }
}
