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
    public class ReceptionTransfertRondBetonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceptionTransfertRondBetonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReceptionTransfertRondBetons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReceptionTransfertRondBetons.Include(r => r.Parc).Include(r => r.TypeDeCamion).Include(r => r.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReceptionTransfertRondBetons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }

            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }

            return View(receptionTransfertRondBeton);
        }

        // GET: ReceptionTransfertRondBetons/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");
            return View();
        }

        // POST: ReceptionTransfertRondBetons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBL,DateOp,Transporteur,Provenance,TypeDeTransportId,TypeDeCamionId,Mat,Diametre,PCC,PCV,PQS,Observation,Termine")] ReceptionTransfertRondBeton receptionTransfertRondBeton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receptionTransfertRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertRondBeton.TypeDeTransportId);
            return View(receptionTransfertRondBeton);
        }

        // GET: ReceptionTransfertRondBetons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }

            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons.FindAsync(id);
            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertRondBeton.TypeDeTransportId);
            return View(receptionTransfertRondBeton);
        }

        // POST: ReceptionTransfertRondBetons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBL,DateOp,Transporteur,Provenance,TypeDeTransportId,TypeDeCamionId,Mat,Diametre,PCC,PCV,PQS,Observation,Termine")] ReceptionTransfertRondBeton receptionTransfertRondBeton)
        {
            if (id != receptionTransfertRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionTransfertRondBeton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionTransfertRondBetonExists(receptionTransfertRondBeton.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertRondBeton.TypeDeTransportId);
            return View(receptionTransfertRondBeton);
        }

        // GET: ReceptionTransfertRondBetons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }

            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }

            return View(receptionTransfertRondBeton);
        }

        // POST: ReceptionTransfertRondBetons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReceptionTransfertRondBetons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReceptionTransfertRondBetons'  is null.");
            }
            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons.FindAsync(id);
            if (receptionTransfertRondBeton != null)
            {
                _context.ReceptionTransfertRondBetons.Remove(receptionTransfertRondBeton);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionTransfertRondBetonExists(int id)
        {
          return (_context.ReceptionTransfertRondBetons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
