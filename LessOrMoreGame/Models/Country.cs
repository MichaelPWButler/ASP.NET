using System.Text.Json;
using System.Text.Json.Serialization;

namespace LessOrMoreGame.Models
{
    public class Country
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int Population { get; set; }
        [JsonPropertyName("FlagImgSrc")]
        public string? ImageSource { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Country>(this);
    }
}
