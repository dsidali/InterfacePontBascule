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
    public class ComPortsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComPortsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComPorts
        public async Task<IActionResult> Index()
        {
              return View(await _context.ComPorts.ToListAsync());
        }

        // GET: ComPorts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ComPorts == null)
            {
                return NotFound();
            }

            var comPort = await _context.ComPorts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comPort == null)
            {
                return NotFound();
            }

            return View(comPort);
        }

        // GET: ComPorts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComPorts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PortName,BaudeRate,DataBits,ReceivedBytesThreshold,DtrEnable,RtsEnable,DureeAttente")] ComPort comPort)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comPort);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comPort);
        }

        // GET: ComPorts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComPorts == null)
            {
                return NotFound();
            }

            var comPort = await _context.ComPorts.FindAsync(id);
            if (comPort == null)
            {
                return NotFound();
            }
            return View(comPort);
        }

        // POST: ComPorts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PortName,BaudeRate,DataBits,ReceivedBytesThreshold,DtrEnable,RtsEnable,DureeAttente")] ComPort comPort)
        {
            if (id != comPort.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comPort);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComPortExists(comPort.Id))
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
            return View(comPort);
        }

        // GET: ComPorts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ComPorts == null)
            {
                return NotFound();
            }

            var comPort = await _context.ComPorts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comPort == null)
            {
                return NotFound();
            }

            return View(comPort);
        }

        // POST: ComPorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ComPorts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ComPorts'  is null.");
            }
            var comPort = await _context.ComPorts.FindAsync(id);
            if (comPort != null)
            {
                _context.ComPorts.Remove(comPort);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComPortExists(int id)
        {
          return _context.ComPorts.Any(e => e.Id == id);
        }
    }
}
