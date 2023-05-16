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
    public class TypeDeCamionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeDeCamionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeDeCamions
        public async Task<IActionResult> Index()
        {
              return _context.TypeDeCamions != null ? 
                          View(await _context.TypeDeCamions.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TypeDeCamions'  is null.");
        }

        // GET: TypeDeCamions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeDeCamions == null)
            {
                return NotFound();
            }

            var typeDeCamion = await _context.TypeDeCamions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDeCamion == null)
            {
                return NotFound();
            }

            return View(typeDeCamion);
        }

        // GET: TypeDeCamions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeDeCamions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeCamion")] TypeDeCamion typeDeCamion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeDeCamion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeDeCamion);
        }

        // GET: TypeDeCamions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypeDeCamions == null)
            {
                return NotFound();
            }

            var typeDeCamion = await _context.TypeDeCamions.FindAsync(id);
            if (typeDeCamion == null)
            {
                return NotFound();
            }
            return View(typeDeCamion);
        }

        // POST: TypeDeCamions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeCamion")] TypeDeCamion typeDeCamion)
        {
            if (id != typeDeCamion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeDeCamion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeDeCamionExists(typeDeCamion.Id))
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
            return View(typeDeCamion);
        }

        // GET: TypeDeCamions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypeDeCamions == null)
            {
                return NotFound();
            }

            var typeDeCamion = await _context.TypeDeCamions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDeCamion == null)
            {
                return NotFound();
            }

            return View(typeDeCamion);
        }

        // POST: TypeDeCamions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypeDeCamions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TypeDeCamions'  is null.");
            }
            var typeDeCamion = await _context.TypeDeCamions.FindAsync(id);
            if (typeDeCamion != null)
            {
                _context.TypeDeCamions.Remove(typeDeCamion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeDeCamionExists(int id)
        {
          return (_context.TypeDeCamions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
