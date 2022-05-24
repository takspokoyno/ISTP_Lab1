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
    public class SponsorsController : Controller
    {
        private readonly RacingContext _context;

        public SponsorsController(RacingContext context)
        {
            _context = context;
        }

        // GET: Sponsors
        public async Task<IActionResult> Index(int? teamId, string name)
        {
            //var racingContext = _context.Sponsors.Include(s => s.Team);
            //return View(await racingContext.ToListAsync());
            if (teamId == null) return RedirectToAction("Cars", "Index");

            ViewBag.TeamName = name;
            var sponsorsByTeam = _context.Sponsors.Include(p => p.Team).Where(p => p.Team.Id == teamId);
            ViewData["currentTeamId"] = teamId;
            List<Sponsor> result = await sponsorsByTeam.ToListAsync();
            return View(result);
        }

        // GET: Sponsors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sponsors == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors
                .Include(s => s.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // GET: Sponsors/Create
        public IActionResult Create(int teamId)
        {
            //ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id");
            ViewData["currentTeam"] = _context.Teams.FirstOrDefault(c => c.Id == teamId);
            return View();
        }

        // POST: Sponsors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TeamId")] Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sponsor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Sponsors", routeValues: new { teamId = sponsor.TeamId });
            }
            //ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", sponsor.TeamId);
            ViewData["currentTeam"] = _context.Teams.FirstOrDefault(c => c.Id == sponsor.TeamId);
            return View(sponsor);
        }

        // GET: Sponsors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sponsors == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", sponsor.TeamId);
            return View(sponsor);
        }

        // POST: Sponsors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TeamId")] Sponsor sponsor)
        {
            if (id != sponsor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponsor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorExists(sponsor.Id))
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
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Id", sponsor.TeamId);
            return View(sponsor);
        }

        // GET: Sponsors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sponsors == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors
                .Include(s => s.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }
        
        // POST: Sponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sponsors == null)
            {
                return Problem("Entity set 'RacingContext.Sponsors'  is null.");
            }
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor != null)
            {
                _context.Sponsors.Remove(sponsor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),"Sponsors", new { teamId = sponsor.TeamId});
        }
        
        private bool SponsorExists(int id)
        {
          return (_context.Sponsors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
