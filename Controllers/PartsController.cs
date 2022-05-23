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
        public async Task<IActionResult> Index(int? carId, string model, string brand)
        {
            if (carId == null) return RedirectToAction("Cars", "Index");

            //ViewBag.CarId = id;
            ViewBag.CarBrand = brand;
            ViewBag.CarModel = model;
            var partsByCar = _context.Parts.Include(p => p.Car).Where(p => p.Car.Id == carId);
            ViewData["currentCarId"]=carId;
            List<Part> result = await partsByCar.ToListAsync();
            return View(result);
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
            ViewData["currentCar"] = _context.Cars.FirstOrDefault(c => c.Id == carId);

            //ViewBag.CarId = carId;
            //ViewBag.CarBrand = _context.Cars.Where(c => c.Id == carId).FirstOrDefault().Brand;
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CarId")] Part part)
        {
            if (ModelState.IsValid)
            {
                _context.Add(part);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Parts", routeValues: new {carId=part.CarId});
                //return RedirectToAction("Index", "Parts", new { id = carId, brand = _context.Cars.Where(c=>c.Id==carId).FirstOrDefault().Brand });
            }
            ViewData["currentCar"] = _context.Cars.FirstOrDefault(c => c.Id == part.CarId);
            //ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand", part.CarId);
            return View(part);
            //return RedirectToAction("Index", "Parts", new { id = carId, brand = _context.Cars.Where(c => c.Id == carId).FirstOrDefault().Brand });
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
            return RedirectToAction(nameof(Index), "Parts", routeValues: new { carId = part.CarId });
        }

        private bool PartExists(int id)
        {
          return (_context.Parts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}