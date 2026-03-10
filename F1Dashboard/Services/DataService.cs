using F1Dashboard.Models;

namespace F1Dashboard.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient _Client;

        public DataService(HttpClient client) 
        { 
            _Client = client;
        }

        public NextRaceModel GetNextRace()
        {
            var _X = _Client.GetFromJsonAsync<NextRaceModel>("https://api.jolpi.ca/ergast/f1/2026/races/").Result;
            return new NextRaceModel();
        }
    }
}
