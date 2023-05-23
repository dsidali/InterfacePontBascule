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
    public class AchatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AchatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Achats
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Achats.Include(a => a.Parc).Include(a => a.TypeDeCamion).Include(a => a.TypeDeDechet).Include(a => a.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Achats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Achats == null)
            {
                return NotFound();
            }

            var achat = await _context.Achats
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeDechet)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achat == null)
            {
                return NotFound();
            }

            return View(achat);
        }

        // GET: Achats/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport");
            return View();
        }

        // POST: Achats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBonA,NumTicket,Mat,Transporteur,Source,TypeDeTransportId,TypeDeCamionId,TypeDeDechetId,DateOP,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] Achat achat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(achat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", achat.TypeDeTransportId);
            return View(achat);
        }

        // GET: Achats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Achats == null)
            {
                return NotFound();
            }

            var achat = await _context.Achats.FindAsync(id);
            if (achat == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", achat.TypeDeTransportId);
            return View(achat);
        }

        // POST: Achats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBonA,NumTicket,Mat,Transporteur,Source,TypeDeTransportId,TypeDeCamionId,TypeDeDechetId,DateOP,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] Achat achat)
        {
            if (id != achat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchatExists(achat.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", achat.TypeDeTransportId);
            return View(achat);
        }

        // GET: Achats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Achats == null)
            {
                return NotFound();
            }

            var achat = await _context.Achats
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeDechet)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achat == null)
            {
                return NotFound();
            }

            return View(achat);
        }

        // POST: Achats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Achats == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Achats'  is null.");
            }
            var achat = await _context.Achats.FindAsync(id);
            if (achat != null)
            {
                _context.Achats.Remove(achat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AchatExists(int id)
        {
          return (_context.Achats?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        public async Task<IActionResult> New()
        {
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(int id)
        {
            return View();
        }


        public async Task<IActionResult> Modifier()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modifier(int id)
        {
            return View();
        }
    }
}
