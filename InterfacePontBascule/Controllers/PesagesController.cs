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
    public class PesagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PesagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pesages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pesages.Include(p => p.Parc).Include(p => p.TypeDeCamion).Include(p => p.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pesages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }

            var pesage = await _context.Pesages
                .Include(p => p.Parc)
                .Include(p => p.TypeDeCamion)
                .Include(p => p.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pesage == null)
            {
                return NotFound();
            }

            return View(pesage);
        }

        // GET: Pesages/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");
            return View();
        }

        // POST: Pesages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBonA,NumTicket,Transporteur,TypeDeTransportId,TypeDeCamionId,Mat,DateOP,PCC,PCV,QP,Observation,Termine")] Pesage pesage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pesage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", pesage.TypeDeTransportId);
            return View(pesage);
        }

        // GET: Pesages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }

            var pesage = await _context.Pesages.FindAsync(id);
            if (pesage == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", pesage.TypeDeTransportId);
            return View(pesage);
        }

        // POST: Pesages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBonA,NumTicket,Transporteur,TypeDeTransportId,TypeDeCamionId,Mat,DateOP,PCC,PCV,QP,Observation,Termine")] Pesage pesage)
        {
            if (id != pesage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pesage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PesageExists(pesage.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", pesage.TypeDeTransportId);
            return View(pesage);
        }

        // GET: Pesages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }

            var pesage = await _context.Pesages
                .Include(p => p.Parc)
                .Include(p => p.TypeDeCamion)
                .Include(p => p.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pesage == null)
            {
                return NotFound();
            }

            return View(pesage);
        }

        // POST: Pesages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pesages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pesages'  is null.");
            }
            var pesage = await _context.Pesages.FindAsync(id);
            if (pesage != null)
            {
                _context.Pesages.Remove(pesage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PesageExists(int id)
        {
          return (_context.Pesages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
