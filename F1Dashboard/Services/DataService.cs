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
            int _CurrentRound = _NextRace.Round - 1;

            var json = await _Client.GetStringAsync("https://api.jolpi.ca/ergast/f1/2026/results/?limit=100");

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var lastFiveRaces = root
                .GetProperty("MRData")
                .GetProperty("RaceTable")
                .GetProperty("Races")
                .EnumerateArray();
            //.TakeLast(5);

            //Dictionary<string, (string FamilyName, string GivenName, double Points)> driverPoints = new Dictionary<string, (string FamilyName, string GivenName, double Points)>();

            //lastFiveRaces = lastFiveRaces.EnumerateArray().TakeLast(5);

            //foreach (var race in lastFiveRaces)
            //{
            //    var results = race.GetProperty("Results");

            //    foreach (var result in results.EnumerateArray())
            //    {
            //        var driver = result.GetProperty("Driver");

            //        var driverId = driver.GetProperty("driverId").GetString()!;
            //        var givenName = driver.GetProperty("givenName").GetString();
            //        var familyName = driver.GetProperty("familyName").GetString();

            //        var points = int.TryParse(result.GetProperty("points").GetString(), out var parsedPoints) ? parsedPoints : 0;

            //        if (driverPoints.ContainsKey(driverId))
            //        {
            //            var current = driverPoints[driverId];
            //            driverPoints[driverId] = (familyName, givenName, current.Points + points);
            //        }
            //        else
            //        {
            //            driverPoints[driverId] = (familyName, givenName, points);
            //        }
            //    }
            //}

            //var topDriver = driverPoints.OrderByDescending(driver => driver.Value.Points).First();

            return new TopDriverLast5Races();

            //return new TopDriverLast5Races
            //{
            //    FamilyName = topDriver.Value.FamilyName,
            //    GivenName = topDriver.Value.GivenName,
            //    Points = topDriver.Value.Points
            //};
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

        private Dictionary<string, (string Name, int Points)> CalculateDriverPoints(List<JsonDocument> races)
        {
            var driverPoints = new Dictionary<string, (string Name, int Points)>();

            foreach (var raceDoc in races)
            {
                var racesArray = raceDoc.RootElement
                    .GetProperty("MRData")
                    .GetProperty("RaceTable")
                    .GetProperty("Races");

                if (racesArray.GetArrayLength() == 0)
                    continue;

                var results = racesArray[0].GetProperty("Results");

                foreach (var result in results.EnumerateArray())
                {
                    var driver = result.GetProperty("Driver");

                    var id = driver.GetProperty("driverId").GetString()!;
                    var name = $"{driver.GetProperty("givenName").GetString()} " +
                               $"{driver.GetProperty("familyName").GetString()}";

                    int points = int.Parse(
                        result.GetProperty("points").GetString()!);

                    if (driverPoints.TryGetValue(id, out var current))
                    {
                        driverPoints[id] =
                            (name, current.Points + points);
                    }
                    else
                    {
                        driverPoints[id] = (name, points);
                    }
                }
            }

            return driverPoints;
        }
    }
}
