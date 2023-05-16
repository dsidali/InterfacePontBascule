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
    public class TypeDeTransportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeDeTransportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeDeTransports
        public async Task<IActionResult> Index()
        {
              return _context.TypeDeTransports != null ? 
                          View(await _context.TypeDeTransports.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TypeDeTransports'  is null.");
        }

        // GET: TypeDeTransports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeDeTransports == null)
            {
                return NotFound();
            }

            var typeDeTransport = await _context.TypeDeTransports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDeTransport == null)
            {
                return NotFound();
            }

            return View(typeDeTransport);
        }

        // GET: TypeDeTransports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeDeTransports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeTransport")] TypeDeTransport typeDeTransport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeDeTransport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeDeTransport);
        }

        // GET: TypeDeTransports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypeDeTransports == null)
            {
                return NotFound();
            }

            var typeDeTransport = await _context.TypeDeTransports.FindAsync(id);
            if (typeDeTransport == null)
            {
                return NotFound();
            }
            return View(typeDeTransport);
        }

        // POST: TypeDeTransports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeTransport")] TypeDeTransport typeDeTransport)
        {
            if (id != typeDeTransport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeDeTransport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeDeTransportExists(typeDeTransport.Id))
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
            return View(typeDeTransport);
        }

        // GET: TypeDeTransports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypeDeTransports == null)
            {
                return NotFound();
            }

            var typeDeTransport = await _context.TypeDeTransports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDeTransport == null)
            {
                return NotFound();
            }

            return View(typeDeTransport);
        }

        // POST: TypeDeTransports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypeDeTransports == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TypeDeTransports'  is null.");
            }
            var typeDeTransport = await _context.TypeDeTransports.FindAsync(id);
            if (typeDeTransport != null)
            {
                _context.TypeDeTransports.Remove(typeDeTransport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeDeTransportExists(int id)
        {
          return (_context.TypeDeTransports?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
