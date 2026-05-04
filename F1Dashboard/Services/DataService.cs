using F1Dashboard.Models;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace F1Dashboard.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient _Client;
        private IWebHostEnvironment WebHostEnvironment { get; }

        public DataService(HttpClient client, IWebHostEnvironment webHostEnvironment) 
        { 
            _Client = client;
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "Data", "Races.json"); }
        }

        public NextRaceModel GetNextRace()
        {
            StreamReader _Reader = File.OpenText(JsonFileName);
            IEnumerable<NextRaceModel>? _Races = new List<NextRaceModel>();

            _Races = JsonSerializer.Deserialize<NextRaceModel[]>(_Reader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

            NextRaceModel _NextRace = _Races?.Where(race => race.Date > DateTime.Now)
                .OrderBy(race => race.Date).FirstOrDefault() ?? new NextRaceModel();

            return _NextRace;
        }

        public async Task<DriverModel> GetDriversLeaderAsync()
        {
            var json = await _Client.GetStringAsync("https://api.jolpi.ca/ergast/f1/2026/driverstandings/");

            using var doc = JsonDocument.Parse(json);

            var root = doc.RootElement;

            var standings = root
                .GetProperty("MRData")
                .GetProperty("StandingsTable")
                .GetProperty("StandingsLists")[0]
                .GetProperty("DriverStandings")
                .EnumerateArray()
                .FirstOrDefault();

            DriverModel Driver = new DriverModel()
            {
                FamilyName = standings.GetProperty("Driver").GetProperty("familyName").GetString() ?? "",
                GivenName = standings.GetProperty("Driver").GetProperty("givenName").GetString() ?? ""
            };

            return Driver;
        }
    }
}
