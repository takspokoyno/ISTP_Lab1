using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labka1.Models;

namespace Labka1.Controllers
{
    public class PartsController : Controller
    {
        private readonly RacingContext _context;

        public PartsController(RacingContext context)
        {
            _context = context;
        }

        // GET: Parts
        public async Task<IActionResult> Index(int? id, string? brand, string? model)
        {
            if (id == null) return RedirectToAction("Cars", "Index");

            ViewBag.CarId = id;
            ViewBag.CarBrand = brand;
            ViewBag.CarModel = model;
            var partsByCar = _context.Parts.Where(p => p.CarId == id).Include(p => p.Car);

            return View(await partsByCar.ToListAsync());
        }

        // GET: Parts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parts == null)
            {
                return NotFound();
            }

            var part = await _context.Parts
                .Include(p => p.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        // GET: Parts/Create
        public IActionResult Create(int carId)
        {
            ViewData["CarId"] = _context.Cars.FirstOrDefault(c => c.Id == carId);
            //ViewBag.CarId = carId;
            //ViewBag.CarBrand = _context.Cars.Where(c => c.Id == carId).FirstOrDefault().Brand;
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int carId, [Bind("Id,Name")] Part part)
        {
            if (ModelState.IsValid)
            {
                _context.Add(part);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Parts", new { id = carId, brand = _context.Cars.Where(c=>c.Id==carId).FirstOrDefault().Brand });
            }
            ViewData["CarId"] = _context.Cars.FirstOrDefault(c => c.Id == part.CarId);
            //ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand", part.CarId);
            //return View(part);
            return RedirectToAction("Index", "Parts", new { id = carId, brand = _context.Cars.Where(c => c.Id == carId).FirstOrDefault().Brand });
        }

        // GET: Parts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parts == null)
            {
                return NotFound();
            }

            var part = await _context.Parts.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand", part.CarId);
            return View(part);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CarId")] Part part)
        {
            if (id != part.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(part);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartExists(part.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand", part.CarId);
            return View(part);
        }

        // GET: Parts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parts == null)
            {
                return NotFound();
            }

            var part = await _context.Parts
                .Include(p => p.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parts == null)
            {
                return Problem("Entity set 'RacingContext.Parts'  is null.");
            }
            var part = await _context.Parts.FindAsync(id);
            if (part != null)
            {
                _context.Parts.Remove(part);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartExists(int id)
        {
          return (_context.Parts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
