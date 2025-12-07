using LessOrMoreGame.Models;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;

namespace LessOrMoreGame.wwwroot.Services
{
    public class JsonFileCountryService
    {
        private IWebHostEnvironment WebHostEnvironment { get; }

        public JsonFileCountryService(IWebHostEnvironment webHostEnvironment) 
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName 
        { 
            get{ return Path.Combine(WebHostEnvironment.WebRootPath, "Data", "Countries.json"); }
        }

        public IEnumerable<Country> GetCountries()
        {
            StreamReader _Reader = File.OpenText(JsonFileName);
            IEnumerable<Country>? _Countries = new List<Country>();

            _Countries = JsonSerializer.Deserialize<Country[]>(_Reader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

            return _Countries ?? new List<Country>();
        }
    }
}
