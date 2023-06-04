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
    public class ReceptionTransfertDechetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceptionTransfertDechetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReceptionTransfertDechets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReceptionTransfertDechets.Include(r => r.Parc).Include(r => r.TypeDeCamion).Include(r => r.TypeDeDechet).Include(r => r.TypeDeTransport);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfdech = "active";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReceptionTransfertDechets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }

            var receptionTransfertDechet = await _context.ReceptionTransfertDechets
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeDechet)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }
            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfdech = "active";
            return View(receptionTransfertDechet);
        }

        // GET: ReceptionTransfertDechets/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");
            return View();
        }

        // POST: ReceptionTransfertDechets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBL,DateOp,Transporteur,Provenance,TypeDeTransportId,TypeDeCamionId,Mat,TypeDeDechetId,PCC,PCV,PQS,Observation,Termine")] ReceptionTransfertDechet receptionTransfertDechet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receptionTransfertDechet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertDechet.TypeDeTransportId);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfdech = "active";


            return View(receptionTransfertDechet);
        }

        // GET: ReceptionTransfertDechets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }

            var receptionTransfertDechet = await _context.ReceptionTransfertDechets.FindAsync(id);
            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertDechet.TypeDeTransportId);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfdech = "active";

            return View(receptionTransfertDechet);
        }

        // POST: ReceptionTransfertDechets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBL,DateOp,Transporteur,Provenance,TypeDeTransportId,TypeDeCamionId,Mat,TypeDeDechetId,PCC,PCV,PQS,Observation,Termine")] ReceptionTransfertDechet receptionTransfertDechet)
        {
            if (id != receptionTransfertDechet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionTransfertDechet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionTransfertDechetExists(receptionTransfertDechet.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertDechet.TypeDeTransportId);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfdech = "active";

            return View(receptionTransfertDechet);
        }

        // GET: ReceptionTransfertDechets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }

            var receptionTransfertDechet = await _context.ReceptionTransfertDechets
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeDechet)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }

            return View(receptionTransfertDechet);
        }

        // POST: ReceptionTransfertDechets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReceptionTransfertDechets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReceptionTransfertDechets'  is null.");
            }
            var receptionTransfertDechet = await _context.ReceptionTransfertDechets.FindAsync(id);
            if (receptionTransfertDechet != null)
            {
                _context.ReceptionTransfertDechets.Remove(receptionTransfertDechet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionTransfertDechetExists(int id)
        {
          return (_context.ReceptionTransfertDechets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
