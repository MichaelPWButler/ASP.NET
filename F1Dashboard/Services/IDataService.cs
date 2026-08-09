using F1Dashboard.Models;

namespace F1Dashboard.Services
{
    public interface IDataService
    {
        RaceModel GetNextRace();

        Task<DriverModel> GetDriversLeaderAsync();

        Task<ConstructorModel> GetConstructorsLeaderAsync();

        Task<int> GetNumberOfRaces();
    }
}