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
    public class TypeProduitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeProduitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeProduits
        public async Task<IActionResult> Index()
        {
              return _context.TypeProduits != null ? 
                          View(await _context.TypeProduits.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TypeProduits'  is null.");
        }

        // GET: TypeProduits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeProduits == null)
            {
                return NotFound();
            }

            var typeProduit = await _context.TypeProduits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeProduit == null)
            {
                return NotFound();
            }

            return View(typeProduit);
        }

        // GET: TypeProduits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeProduits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeDeProduit")] TypeProduit typeProduit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeProduit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeProduit);
        }

        // GET: TypeProduits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypeProduits == null)
            {
                return NotFound();
            }

            var typeProduit = await _context.TypeProduits.FindAsync(id);
            if (typeProduit == null)
            {
                return NotFound();
            }
            return View(typeProduit);
        }

        // POST: TypeProduits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeDeProduit")] TypeProduit typeProduit)
        {
            if (id != typeProduit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeProduit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeProduitExists(typeProduit.Id))
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
            return View(typeProduit);
        }

        // GET: TypeProduits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypeProduits == null)
            {
                return NotFound();
            }

            var typeProduit = await _context.TypeProduits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeProduit == null)
            {
                return NotFound();
            }

            return View(typeProduit);
        }

        // POST: TypeProduits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypeProduits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TypeProduits'  is null.");
            }
            var typeProduit = await _context.TypeProduits.FindAsync(id);
            if (typeProduit != null)
            {
                _context.TypeProduits.Remove(typeProduit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeProduitExists(int id)
        {
          return (_context.TypeProduits?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
