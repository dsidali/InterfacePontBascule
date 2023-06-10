using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using InterfacePontBascule.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterfacePontBascule.Data;
using InterfacePontBascule.Models;

namespace InterfacePontBascule.Controllers
{
    public class SortieTransfertDechetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private IComPortUsage _comPortUsage;
        private INumTicketBonManagement _numTicketBonManagement;

        public SortieTransfertDechetsController(ApplicationDbContext context, IComPortUsage comPortUsage, IWebHostEnvironment webHostEnvironment, INumTicketBonManagement numTicketBonManagement)
        {
            _context = context;
            _comPortUsage = comPortUsage;
            _webHostEnvironment = webHostEnvironment;
            _numTicketBonManagement = numTicketBonManagement;
        }

        // GET: SortieTransfertDechets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SortieTransfertDechets.Where(a=>a.Termine==false).Include(s => s.Parc).Include(s => s.TypeDeCamion).Include(s => s.TypeDeDechet).Include(s => s.TypeDeTransport);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfdech = "active";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SortieTransfertDechets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SortieTransfertDechets == null)
            {
                return NotFound();
            }

            var sortieTransfertDechet = await _context.SortieTransfertDechets
                .Include(s => s.Parc)
                .Include(s => s.TypeDeCamion)
                .Include(s => s.TypeDeDechet)
                .Include(s => s.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieTransfertDechet == null)
            {
                return NotFound();
            }
            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfdech = "active";
            return View(sortieTransfertDechet);
        }

        // GET: SortieTransfertDechets/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfdech = "active";
            return View();
        }

        // POST: SortieTransfertDechets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SortieTransfertDechet sortieTransfertDechet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sortieTransfertDechet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", sortieTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieTransfertDechet.TypeDeTransportId);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfdech = "active";
            return View(sortieTransfertDechet);
        }

        // GET: SortieTransfertDechets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SortieTransfertDechets == null)
            {
                return NotFound();
            }

            var sortieTransfertDechet = await _context.SortieTransfertDechets.FindAsync(id);
            if (sortieTransfertDechet == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", sortieTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieTransfertDechet.TypeDeTransportId);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfdech = "active";
            return View(sortieTransfertDechet);
        }

        // POST: SortieTransfertDechets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SortieTransfertDechet sortieTransfertDechet)
        {
            if (id != sortieTransfertDechet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieTransfertDechet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieTransfertDechetExists(sortieTransfertDechet.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", sortieTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieTransfertDechet.TypeDeTransportId);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfdech = "active";

            return View(sortieTransfertDechet);
        }

        // GET: SortieTransfertDechets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SortieTransfertDechets == null)
            {
                return NotFound();
            }

            var sortieTransfertDechet = await _context.SortieTransfertDechets
                .Include(s => s.Parc)
                .Include(s => s.TypeDeCamion)
                .Include(s => s.TypeDeDechet)
                .Include(s => s.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieTransfertDechet == null)
            {
                return NotFound();
            }
            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfdech = "active";
            return View(sortieTransfertDechet);
        }

        // POST: SortieTransfertDechets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SortieTransfertDechets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SortieTransfertDechets'  is null.");
            }
            var sortieTransfertDechet = await _context.SortieTransfertDechets.FindAsync(id);
            if (sortieTransfertDechet != null)
            {
                _context.SortieTransfertDechets.Remove(sortieTransfertDechet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SortieTransfertDechetExists(int id)
        {
          return (_context.SortieTransfertDechets?.Any(e => e.Id == id)).GetValueOrDefault();
        }





        public async Task<IActionResult> New()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport");


            ViewBag.e = User.Identity.Name;

            var maxNumBon = _context.SortieTransfertDechets.Max(x => x.NumBL);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);

            ViewBag.sortieTransfertDechets = "active";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(SortieTransfertDechet sortieTransfertDechet)
        {
            if (ModelState.IsValid)
            {
                var y = _context.Add(sortieTransfertDechet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = y.Entity.Id });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet", sortieTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieTransfertDechet.TypeDeTransportId);

            var maxNumBon = _context.SortieTransfertDechets.Max(x => x.NumBL);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);
            ViewBag.sortieTransfertDechets = "active";
            return View(sortieTransfertDechet);
        }






        public async Task<IActionResult> Reprise(int? id)
        {

            if (id == null || _context.SortieTransfertDechets == null)
            {
                return NotFound();
            }

            var sortieTransfertDechet = await _context.SortieTransfertDechets.FindAsync(id);
            if (sortieTransfertDechet == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet", sortieTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieTransfertDechet.TypeDeTransportId);
            ViewBag.sortieTransfertDechets = "active";
            return View(sortieTransfertDechet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reprise(int id, SortieTransfertDechet sortieTransfertDechet)
        {
            if (id != sortieTransfertDechet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieTransfertDechet);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieTransfertDechetExists(sortieTransfertDechet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = sortieTransfertDechet.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet", sortieTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieTransfertDechet.TypeDeTransportId);
            ViewBag.sortieTransfertDechets = "active";
            return View(sortieTransfertDechet);
        }



        public async Task<IActionResult> ListFinished()
        {
            var applicationDbContext = _context.SortieTransfertDechets.Where(a => a.Termine == true).Include(a => a.Parc).Include(a => a.TypeDeCamion).Include(a => a.TypeDeDechet).Include(a => a.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetBon(int? id)
        {
            if (id == null || _context.SortieTransfertDechets == null)
            {
                return NotFound();
            }
            var sortieTransfertDechet = await _context.SortieTransfertDechets.FirstOrDefaultAsync(m => m.Id == id);

            if (sortieTransfertDechet == null)
            {
                return NotFound();
            }

            if (sortieTransfertDechet.Termine)
            {
                return RedirectToAction(nameof(BonReceptionSortieTransfertDechet), new { id = sortieTransfertDechet.Id });

            }
            else
            {
                return RedirectToAction(nameof(BonDechargement), new { id = sortieTransfertDechet.Id });

            }
        }


        public async Task<IActionResult> BonDechargement(int? id)
        {
            if (id == null || _context.SortieTransfertDechets == null)
            {
                return NotFound();
            }

            var sortieTransfertDechet = await _context.SortieTransfertDechets
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeDechet)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieTransfertDechet == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionSortieTransfertDechet.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");



           

            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


        public async Task<IActionResult> BonReceptionSortieTransfertDechet(int id)
        {
            if (id == null || _context.SortieTransfertDechets == null)
            {
                return NotFound();
            }

            var sortieTransfertDechet = await _context.SortieTransfertDechets
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeDechet)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieTransfertDechet == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionSortieTransfertDechet.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");



    
            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }



        public ActionResult Peser()
        {
            return Content(_comPortUsage.ReadData());
        }
    }
}
