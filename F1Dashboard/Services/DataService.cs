using F1Dashboard.Models;
using System.Text.Json;

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
    }
}
