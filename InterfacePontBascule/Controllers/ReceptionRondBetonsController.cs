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
    public class ReceptionRondBetonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceptionRondBetonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReceptionRondBetons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReceptionRondBetons.Include(r => r.Parc).Include(r => r.TypeDeCamion).Include(r => r.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReceptionRondBetons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReceptionRondBetons == null)
            {
                return NotFound();
            }

            var receptionRondBeton = await _context.ReceptionRondBetons
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionRondBeton == null)
            {
                return NotFound();
            }

            return View(receptionRondBeton);
        }

        // GET: ReceptionRondBetons/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");
            return View();
        }

        // POST: ReceptionRondBetons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBonA,NumTicket,DateOp,Source,Transporteur,Mat,TypeDeTransportId,TypeDeCamionId,Diametre,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] ReceptionRondBeton receptionRondBeton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receptionRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionRondBeton.TypeDeTransportId);
            return View(receptionRondBeton);
        }

        // GET: ReceptionRondBetons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReceptionRondBetons == null)
            {
                return NotFound();
            }

            var receptionRondBeton = await _context.ReceptionRondBetons.FindAsync(id);
            if (receptionRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionRondBeton.TypeDeTransportId);
            return View(receptionRondBeton);
        }

        // POST: ReceptionRondBetons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBonA,NumTicket,DateOp,Source,Transporteur,Mat,TypeDeTransportId,TypeDeCamionId,Diametre,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] ReceptionRondBeton receptionRondBeton)
        {
            if (id != receptionRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionRondBeton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionRondBetonExists(receptionRondBeton.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionRondBeton.TypeDeTransportId);
            return View(receptionRondBeton);
        }

        // GET: ReceptionRondBetons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReceptionRondBetons == null)
            {
                return NotFound();
            }

            var receptionRondBeton = await _context.ReceptionRondBetons
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionRondBeton == null)
            {
                return NotFound();
            }

            return View(receptionRondBeton);
        }

        // POST: ReceptionRondBetons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReceptionRondBetons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReceptionRondBetons'  is null.");
            }
            var receptionRondBeton = await _context.ReceptionRondBetons.FindAsync(id);
            if (receptionRondBeton != null)
            {
                _context.ReceptionRondBetons.Remove(receptionRondBeton);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionRondBetonExists(int id)
        {
          return (_context.ReceptionRondBetons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
