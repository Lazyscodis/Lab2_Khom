using Lab2.Models;

namespace Lab2.Repositories
{
    public interface IStationRepository
    {
        Task<IEnumerable<StationModel>> GetAllAsync();
        Task<StationModel> GetByIdAsync(int id);
        Task AddAsync(StationModel station);
        Task UpdateAsync(StationModel station);
        Task DeleteAsync(int id);
    }
}