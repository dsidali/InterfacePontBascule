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
    public class TypeDeDechetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeDeDechetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeDeDechets
        public async Task<IActionResult> Index()
        {
              return _context.TypeDeDechets != null ? 
                          View(await _context.TypeDeDechets.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TypeDeDechets'  is null.");
        }

        // GET: TypeDeDechets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeDeDechets == null)
            {
                return NotFound();
            }

            var typeDeDechet = await _context.TypeDeDechets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDeDechet == null)
            {
                return NotFound();
            }

            return View(typeDeDechet);
        }

        // GET: TypeDeDechets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeDeDechets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeDechet")] TypeDeDechet typeDeDechet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeDeDechet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeDeDechet);
        }

        // GET: TypeDeDechets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypeDeDechets == null)
            {
                return NotFound();
            }

            var typeDeDechet = await _context.TypeDeDechets.FindAsync(id);
            if (typeDeDechet == null)
            {
                return NotFound();
            }
            return View(typeDeDechet);
        }

        // POST: TypeDeDechets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeDechet")] TypeDeDechet typeDeDechet)
        {
            if (id != typeDeDechet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeDeDechet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeDeDechetExists(typeDeDechet.Id))
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
            return View(typeDeDechet);
        }

        // GET: TypeDeDechets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypeDeDechets == null)
            {
                return NotFound();
            }

            var typeDeDechet = await _context.TypeDeDechets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDeDechet == null)
            {
                return NotFound();
            }

            return View(typeDeDechet);
        }

        // POST: TypeDeDechets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypeDeDechets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TypeDeDechets'  is null.");
            }
            var typeDeDechet = await _context.TypeDeDechets.FindAsync(id);
            if (typeDeDechet != null)
            {
                _context.TypeDeDechets.Remove(typeDeDechet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeDeDechetExists(int id)
        {
          return (_context.TypeDeDechets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
