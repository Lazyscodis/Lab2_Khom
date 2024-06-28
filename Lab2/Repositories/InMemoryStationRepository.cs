using Lab2.Data;
using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Repositories
{
    public class InMemoryStationRepository : IStationRepository
    {
        private static List<StationModel> stations = new List<StationModel>();
        private readonly ApplicationDbContext _context;

        public InMemoryStationRepository(ApplicationDbContext context)
        {
            _context = context;
            LoadStationsFromDatabase().Wait();
        }

        private async Task LoadStationsFromDatabase()
        {
            if (!stations.Any())
            {
                stations = await _context.Stations.ToListAsync();
            }
        }

        public Task<IEnumerable<StationModel>> GetAllAsync()
        {
            return Task.FromResult(stations.AsEnumerable());
        }

        public Task<StationModel> GetByIdAsync(int id)
        {
            var station = stations.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(station);
        }

        public async Task AddAsync(StationModel station)
        {
            station.Id = stations.Count > 0 ? stations.Max(s => s.Id) + 1 : 1;
            stations.Add(station);
            _context.Stations.Add(station);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StationModel station)
        {
            var existingStation = stations.FirstOrDefault(s => s.Id == station.Id);
            if (existingStation != null)
            {
                existingStation.Name = station.Name;
                existingStation.City = station.City;
                existingStation.Schedule = station.Schedule;
                existingStation.Platforms = station.Platforms;
                existingStation.OpeningYear = station.OpeningYear;

                _context.Stations.Update(existingStation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var station = stations.FirstOrDefault(s => s.Id == id);
            if (station != null)
            {
                stations.Remove(station);
                _context.Stations.Remove(station);
                await _context.SaveChangesAsync();
            }
        }
    }
}
