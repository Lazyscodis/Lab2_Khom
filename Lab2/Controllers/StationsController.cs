using Lab2.Models;
using Lab2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    public class StationsController : Controller
    {
        private readonly IStationRepository _stationRepository;

        public StationsController(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var stations = await _stationRepository.GetAllAsync();
            return View(stations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,City,Schedule,Platforms,OpeningYear")] StationModel station)
        {
            if (ModelState.IsValid)
            {
                await _stationRepository.AddAsync(station);
                return RedirectToAction(nameof(Index));
            }
            return View(station);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _stationRepository.GetByIdAsync(id.Value);
            if (station == null)
            {
                return NotFound();
            }
            return View(station);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,Schedule,Platforms,OpeningYear")] StationModel station)
        {
            if (id != station.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _stationRepository.UpdateAsync(station);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _stationRepository.GetByIdAsync(station.Id) == null)
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
            return View(station);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _stationRepository.GetByIdAsync(id.Value);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _stationRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
