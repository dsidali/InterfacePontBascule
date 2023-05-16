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
    public class ParcsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParcsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parcs
        public async Task<IActionResult> Index()
        {
              return _context.Parcs != null ? 
                          View(await _context.Parcs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Parcs'  is null.");
        }

        // GET: Parcs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parcs == null)
            {
                return NotFound();
            }

            var parc = await _context.Parcs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parc == null)
            {
                return NotFound();
            }

            return View(parc);
        }

        // GET: Parcs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Adresse,Email,Telephone,Observation,actuel")] Parc parc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parc);
        }

        // GET: Parcs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parcs == null)
            {
                return NotFound();
            }

            var parc = await _context.Parcs.FindAsync(id);
            if (parc == null)
            {
                return NotFound();
            }
            return View(parc);
        }

        // POST: Parcs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Adresse,Email,Telephone,Observation,actuel")] Parc parc)
        {
            if (id != parc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcExists(parc.Id))
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
            return View(parc);
        }

        // GET: Parcs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parcs == null)
            {
                return NotFound();
            }

            var parc = await _context.Parcs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parc == null)
            {
                return NotFound();
            }

            return View(parc);
        }

        // POST: Parcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parcs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Parcs'  is null.");
            }
            var parc = await _context.Parcs.FindAsync(id);
            if (parc != null)
            {
                _context.Parcs.Remove(parc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcExists(int id)
        {
          return (_context.Parcs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
