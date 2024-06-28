using Lab2.Data;
using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Repositories
{
    public class InMemoryTicketRepository : ITicketRepository
    {
        private static List<TicketModel> tickets = new List<TicketModel>();
        private readonly ApplicationDbContext _context;

        public InMemoryTicketRepository(ApplicationDbContext context)
        {
            _context = context;
            LoadTicketsFromDatabase().Wait();
        }

        private async Task LoadTicketsFromDatabase()
        {
            if (!tickets.Any())
            {
                tickets = await _context.Tickets.Include(t => t.Station).ToListAsync();
            }
        }

        public Task<IEnumerable<TicketModel>> GetAllAsync()
        {
            return Task.FromResult(tickets.AsEnumerable());
        }

        public Task<TicketModel> GetByIdAsync(int id)
        {
            var ticket = tickets.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(ticket);
        }

        public async Task AddAsync(TicketModel ticket)
        {
            ticket.Id = tickets.Count > 0 ? tickets.Max(t => t.Id) + 1 : 1;
            tickets.Add(ticket);
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TicketModel ticket)
        {
            var existingTicket = tickets.FirstOrDefault(t => t.Id == ticket.Id);
            if (existingTicket != null)
            {
                existingTicket.Route = ticket.Route;
                existingTicket.Class = ticket.Class;
                existingTicket.Price = ticket.Price;
                existingTicket.StationId = ticket.StationId;

                _context.Tickets.Update(existingTicket);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = tickets.FirstOrDefault(t => t.Id == id);
            if (ticket != null)
            {
                tickets.Remove(ticket);
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
