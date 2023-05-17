using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterfacePontBascule.Data;
using InterfacePontBascule.Models;

namespace InterfacePontBascule.Controllers
{
    public class SortieRondBetonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SortieRondBetonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SortieRondBetons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SortieRondBetons.Include(s => s.Parc).Include(s => s.TypeDeCamion).Include(s => s.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SortieRondBetons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }

            var sortieRondBeton = await _context.SortieRondBetons
                .Include(s => s.Parc)
                .Include(s => s.TypeDeCamion)
                .Include(s => s.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieRondBeton == null)
            {
                return NotFound();
            }

            return View(sortieRondBeton);
        }

        // GET: SortieRondBetons/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");
            return View();
        }

        // POST: SortieRondBetons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBonA,NumTicket,DateOp,Source,Destination,Transporteur,TypeDeTransportId,TypeDeCamionId,Mat,Diametre,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] SortieRondBeton sortieRondBeton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sortieRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieRondBeton.TypeDeTransportId);
            return View(sortieRondBeton);
        }

        // GET: SortieRondBetons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }

            var sortieRondBeton = await _context.SortieRondBetons.FindAsync(id);
            if (sortieRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieRondBeton.TypeDeTransportId);
            return View(sortieRondBeton);
        }

        // POST: SortieRondBetons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBonA,NumTicket,DateOp,Source,Destination,Transporteur,TypeDeTransportId,TypeDeCamionId,Mat,Diametre,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] SortieRondBeton sortieRondBeton)
        {
            if (id != sortieRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieRondBeton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieRondBetonExists(sortieRondBeton.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieRondBeton.TypeDeTransportId);
            return View(sortieRondBeton);
        }

        // GET: SortieRondBetons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }

            var sortieRondBeton = await _context.SortieRondBetons
                .Include(s => s.Parc)
                .Include(s => s.TypeDeCamion)
                .Include(s => s.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieRondBeton == null)
            {
                return NotFound();
            }

            return View(sortieRondBeton);
        }

        // POST: SortieRondBetons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SortieRondBetons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SortieRondBetons'  is null.");
            }
            var sortieRondBeton = await _context.SortieRondBetons.FindAsync(id);
            if (sortieRondBeton != null)
            {
                _context.SortieRondBetons.Remove(sortieRondBeton);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SortieRondBetonExists(int id)
        {
          return (_context.SortieRondBetons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
