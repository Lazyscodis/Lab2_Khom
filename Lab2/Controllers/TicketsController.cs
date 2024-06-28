using Lab2.Models;
using Lab2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IStationRepository _stationRepository;

        public TicketsController(ITicketRepository ticketRepository, IStationRepository stationRepository)
        {
            _ticketRepository = ticketRepository;
            _stationRepository = stationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return View(tickets);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Stations = new SelectList(await _stationRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Route,Class,Price,StationId")] TicketModel ticket)
        {
            if (ModelState.IsValid)
            {
                await _ticketRepository.AddAsync(ticket);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Stations = new SelectList(await _stationRepository.GetAllAsync(), "Id", "Name", ticket.StationId);
            return View(ticket);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketRepository.GetByIdAsync(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewBag.Stations = new SelectList(await _stationRepository.GetAllAsync(), "Id", "Name", ticket.StationId);
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Route,Class,Price,StationId")] TicketModel ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ticketRepository.UpdateAsync(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _ticketRepository.GetByIdAsync(ticket.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Stations = new SelectList(await _stationRepository.GetAllAsync(), "Id", "Name", ticket.StationId);
            return View(ticket);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketRepository.GetByIdAsync(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
