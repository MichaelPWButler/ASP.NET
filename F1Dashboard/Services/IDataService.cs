using F1Dashboard.Models;

namespace F1Dashboard.Services
{
    public interface IDataService
    {
        NextRaceModel GetNextRace();
    }
}