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
    public class StockRbsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockRbsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StockRbs
        public async Task<IActionResult> Index()
        {
              return View(await _context.StockRb.ToListAsync());
        }

        // GET: StockRbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockRb == null)
            {
                return NotFound();
            }

            var stockRb = await _context.StockRb
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockRb == null)
            {
                return NotFound();
            }

            return View(stockRb);
        }

        // GET: StockRbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockRbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Qte")] StockRb stockRb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockRb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockRb);
        }

        // GET: StockRbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockRb == null)
            {
                return NotFound();
            }

            var stockRb = await _context.StockRb.FindAsync(id);
            if (stockRb == null)
            {
                return NotFound();
            }
            return View(stockRb);
        }

        // POST: StockRbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Qte")] StockRb stockRb)
        {
            if (id != stockRb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockRb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockRbExists(stockRb.Id))
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
            return View(stockRb);
        }

        // GET: StockRbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockRb == null)
            {
                return NotFound();
            }

            var stockRb = await _context.StockRb
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockRb == null)
            {
                return NotFound();
            }

            return View(stockRb);
        }

        // POST: StockRbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockRb == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StockRb'  is null.");
            }
            var stockRb = await _context.StockRb.FindAsync(id);
            if (stockRb != null)
            {
                _context.StockRb.Remove(stockRb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockRbExists(int id)
        {
          return _context.StockRb.Any(e => e.Id == id);
        }
    }
}
