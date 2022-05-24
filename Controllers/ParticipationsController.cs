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
    public class ParticipationsController : Controller
    {
        private readonly RacingContext _context;

        public ParticipationsController(RacingContext context)
        {
            _context = context;
        }

        // GET: Participations
        public async Task<IActionResult> Index(int tournamentId, string tournamentName)
        {
            ViewBag.TournamentName = tournamentName;
/*            var Racer = _context.Racers.Select(r=>r);
            var Team = from team in _context.Teams
                       join r in Racer on team.Id equals r.TeamId
                       select new { Id = r.Id, Name = team.Name };
            ViewData["RacerTeam"] = new SelectList(Team, "Id", "Name" );*/
            var racingContext = _context.Participations.Include(p => p.Racer).Include(p => p.Tournament).Where(p=>p.TournamentId == tournamentId);
            return View(await racingContext.ToListAsync());
        }

        // GET: Participations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Participations == null)
            {
                return NotFound();
            }

            var participation = await _context.Participations
                .Include(p => p.Racer)
                .Include(p => p.Tournament)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participation == null)
            {
                return NotFound();
            }

            return View(participation);
        }

        // GET: Participations/Create
        public IActionResult Create()
        {
            ViewData["RacerId"] = new SelectList(_context.Racers, "Id", "Id");
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id");
            return View();
        }

        // POST: Participations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RacerId,TournamentId")] Participation participation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RacerId"] = new SelectList(_context.Racers, "Id", "Id", participation.RacerId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id", participation.TournamentId);
            return View(participation);
        }

        // GET: Participations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Participations == null)
            {
                return NotFound();
            }

            var participation = await _context.Participations.FindAsync(id);
            if (participation == null)
            {
                return NotFound();
            }
            ViewData["RacerId"] = new SelectList(_context.Racers, "Id", "Id", participation.RacerId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id", participation.TournamentId);
            return View(participation);
        }

        // POST: Participations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RacerId,TournamentId")] Participation participation)
        {
            if (id != participation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipationExists(participation.Id))
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
            ViewData["RacerId"] = new SelectList(_context.Racers, "Id", "Id", participation.RacerId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Id", participation.TournamentId);
            return View(participation);
        }

        // GET: Participations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Participations == null)
            {
                return NotFound();
            }

            var participation = await _context.Participations
                .Include(p => p.Racer)
                .Include(p => p.Tournament)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participation == null)
            {
                return NotFound();
            }

            return View(participation);
        }

        // POST: Participations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Participations == null)
            {
                return Problem("Entity set 'RacingContext.Participations'  is null.");
            }
            var participation = await _context.Participations.FindAsync(id);
            if (participation != null)
            {
                _context.Participations.Remove(participation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipationExists(int id)
        {
          return (_context.Participations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
