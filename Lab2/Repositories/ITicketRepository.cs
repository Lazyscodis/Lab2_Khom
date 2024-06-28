using Lab2.Models;

namespace Lab2.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<TicketModel>> GetAllAsync();
        Task<TicketModel> GetByIdAsync(int id);
        Task AddAsync(TicketModel ticket);
        Task UpdateAsync(TicketModel ticket);
        Task DeleteAsync(int id);
    }
}