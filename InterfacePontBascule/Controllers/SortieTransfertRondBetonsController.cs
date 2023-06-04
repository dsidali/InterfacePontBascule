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
    public class SortieTransfertRondBetonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SortieTransfertRondBetonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SortieTransfertRondBetons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SortieTransfertRondBetons.Include(s => s.Parc).Include(s => s.TypeDeCamion).Include(s => s.TypeDeTransport);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SortieTransfertRondBetons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }

            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons
                .Include(s => s.Parc)
                .Include(s => s.TypeDeCamion)
                .Include(s => s.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";
            return View(sortieTransfertRondBeton);
        }

        // GET: SortieTransfertRondBetons/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";
            return View();
        }

        // POST: SortieTransfertRondBetons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBL,DateOp,Transporteur,Destination,TypeDeTransportId,TypeDeCamionId,Mat,Diametre,PCC,PCV,PQS,Observation,Termine")] SortieTransfertRondBeton sortieTransfertRondBeton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sortieTransfertRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieTransfertRondBeton.TypeDeTransportId);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";
            return View(sortieTransfertRondBeton);
        }

        // GET: SortieTransfertRondBetons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }

            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons.FindAsync(id);
            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieTransfertRondBeton.TypeDeTransportId);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";

            return View(sortieTransfertRondBeton);
        }

        // POST: SortieTransfertRondBetons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBL,DateOp,Transporteur,Destination,TypeDeTransportId,TypeDeCamionId,Mat,Diametre,PCC,PCV,PQS,Observation,Termine")] SortieTransfertRondBeton sortieTransfertRondBeton)
        {
            if (id != sortieTransfertRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieTransfertRondBeton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieTransfertRondBetonExists(sortieTransfertRondBeton.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieTransfertRondBeton.TypeDeTransportId);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";

            return View(sortieTransfertRondBeton);
        }

        // GET: SortieTransfertRondBetons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }

            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons
                .Include(s => s.Parc)
                .Include(s => s.TypeDeCamion)
                .Include(s => s.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }

            return View(sortieTransfertRondBeton);
        }

        // POST: SortieTransfertRondBetons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SortieTransfertRondBetons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SortieTransfertRondBetons'  is null.");
            }
            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons.FindAsync(id);
            if (sortieTransfertRondBeton != null)
            {
                _context.SortieTransfertRondBetons.Remove(sortieTransfertRondBeton);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SortieTransfertRondBetonExists(int id)
        {
          return (_context.SortieTransfertRondBetons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
