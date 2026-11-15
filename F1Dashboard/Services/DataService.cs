using F1Dashboard.Models;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace F1Dashboard.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient _Client;
        private IEnumerable<RaceModel>? _Races;
        private IWebHostEnvironment WebHostEnvironment { get; }
        private RaceModel _NextRace;

        public DataService(HttpClient client, IWebHostEnvironment webHostEnvironment) 
        { 
            _Client = client;
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "Data", "Races.json"); }
        }

        public RaceModel GetNextRace()
        {
            StreamReader _Reader = File.OpenText(JsonFileName);
            
            _Races = JsonSerializer.Deserialize<RaceModel[]>(_Reader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

            _NextRace = _Races?.Where(race => race.Date > DateTime.Now)
                .OrderBy(race => race.Date).FirstOrDefault() ?? new RaceModel();

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
                FamilyName = standings.GetProperty("Driver").GetProperty("familyName").GetString()?.ToLower() ?? "",
                GivenName = standings.GetProperty("Driver").GetProperty("givenName").GetString()?.ToLower() ?? ""
            };

            return Driver;
        }

        public async Task<ConstructorModel> GetConstructorsLeaderAsync()
        {
            var json = await _Client.GetStringAsync("https://api.jolpi.ca/ergast/f1/2026/constructorstandings/");

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var standings = root
                .GetProperty("MRData")
                .GetProperty("StandingsTable")
                .GetProperty("StandingsLists")[0]
                .GetProperty("ConstructorStandings")
                .EnumerateArray()
                .FirstOrDefault();
            ConstructorModel Constructor = new ConstructorModel()
            {
                Name = standings.GetProperty("Constructor").GetProperty("name").GetString()?.ToLower() ?? ""
            };
            return Constructor;
        }

        public async Task<int> GetNumberOfRaces()
        {
            return _Races?.Where(race => race.Date > DateTime.Now).Count() ?? 0;
        }

        public async Task<TopDriverLast5Races> GetMostPointsInLastFiveRaces()
        {

            var races = await GetLastFiveRaceResultsAsync();

            var driverPoints = CalculateDriverPoints(races);

            var topDriver = driverPoints
                .OrderByDescending(d => d.Value.Points)
                .FirstOrDefault();


            return new TopDriverLast5Races
            {
                GivenName = topDriver.Value.GivenName,
                FamilyName = topDriver.Value.FamilyName,
                Points = topDriver.Value.Points
            };
        }

        private async Task<List<JsonDocument>> GetLastFiveRaceResultsAsync()
        {
            var races = new List<JsonDocument>();

            int currentRound = _NextRace.Round - 1;

            for (int i = 0; i < 5; i++)
            {
                int round = currentRound - i;

                if (round < 1)
                    break;

                var url = $"https://api.jolpi.ca/ergast/f1/2026/{round}/results/";

                var json = await _Client.GetStringAsync(url);

                races.Add(JsonDocument.Parse(json));
            }

            return races;
        }

        private Dictionary<string, (string GivenName, string FamilyName, int Points)> CalculateDriverPoints(List<JsonDocument> races)
        {
            var driverPoints = new Dictionary<string, (string GivenName, string FamilyName, int Points)>();

            foreach (var raceDoc in races)
            {
                var racesArray = raceDoc.RootElement
                    .GetProperty("MRData")
                    .GetProperty("RaceTable")
                    .GetProperty("Races");

                var results = racesArray[0].GetProperty("Results");

                foreach (var result in results.EnumerateArray())
                {
                    var driver = result.GetProperty("Driver");

                    string id = driver.GetProperty("driverId").GetString()!;
                    string GivenName = driver.GetProperty("givenName").GetString();
                    string FamilyName = driver.GetProperty("familyName").GetString();

                    int points = int.Parse(result.GetProperty("points").GetString()!);

                    if (driverPoints.TryGetValue(id, out var current))
                    {
                        driverPoints[id] =
                            (GivenName, FamilyName, current.Points + points);
                    }
                    else
                    {
                        driverPoints[id] = (GivenName, FamilyName, points);
                    }
                }
            }

            return driverPoints;
        }
    }
}
