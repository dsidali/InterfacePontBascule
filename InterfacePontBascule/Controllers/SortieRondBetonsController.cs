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
    public class SortieRondBetonsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        // private readonly ILogger _logger;
        private IComPortUsage _comPortUsage;
        private INumTicketBonManagement _numTicketBonManagement;

        public SortieRondBetonsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IComPortUsage comPortUsage, INumTicketBonManagement numTicketBonManagement)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _comPortUsage = comPortUsage;
            _numTicketBonManagement = numTicketBonManagement;
        }

        // GET: SortieRondBetons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SortieRondBetons.Where(a=>a.Termine==false).Include(s => s.TypeDeCamion).Include(s => s.TypeDeTransport);

            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbsrt = "active";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SortieRondBetons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }

            var sortieRondBeton = await _context.SortieRondBetons
                .Include(s => s.Parc)
                .Include(s => s.TypeDeCamion)
                .Include(s => s.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieRondBeton == null)
            {
                return NotFound();
            }
            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbsrt = "active";

            return View(sortieRondBeton);
        }

        // GET: SortieRondBetons/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");

            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbsrt = "active";
            return View();
        }

        // POST: SortieRondBetons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBonA,NumTicket,DateOp,Source,Destination,Transporteur,TypeDeTransportId,TypeDeCamionId,Mat,Diametre,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] SortieRondBeton sortieRondBeton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sortieRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieRondBeton.TypeDeTransportId);


            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbsrt = "active";
            return View(sortieRondBeton);
        }

        // GET: SortieRondBetons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }

            var sortieRondBeton = await _context.SortieRondBetons.FindAsync(id);
            if (sortieRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieRondBeton.TypeDeTransportId);


            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbsrt = "active";

            return View(sortieRondBeton);
        }

        // POST: SortieRondBetons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SortieRondBeton sortieRondBeton)
        {
            if (id != sortieRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieRondBeton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieRondBetonExists(sortieRondBeton.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieRondBeton.TypeDeTransportId);


            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbsrt = "active";
            return View(sortieRondBeton);
        }

        // GET: SortieRondBetons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }

            var sortieRondBeton = await _context.SortieRondBetons
                .Include(s => s.Parc)
                .Include(s => s.TypeDeCamion)
                .Include(s => s.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieRondBeton == null)
            {
                return NotFound();
            }


            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbsrt = "active";
            return View(sortieRondBeton);
        }

        // POST: SortieRondBetons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SortieRondBetons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SortieRondBetons'  is null.");
            }
            var sortieRondBeton = await _context.SortieRondBetons.FindAsync(id);
            if (sortieRondBeton != null)
            {
                _context.SortieRondBetons.Remove(sortieRondBeton);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SortieRondBetonExists(int id)
        {
          return (_context.SortieRondBetons?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        public async Task<IActionResult> New()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport");


            ViewBag.e = User.Identity.Name;

            var maxNumBon = _context.SortieRondBetons.Max(x => x.NumBonA);
            var maxNumTicket = _context.SortieRondBetons.Max(x => x.NumTicket);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);
            ViewBag.NumTicket = _numTicketBonManagement.GenerateNextNum(maxNumTicket);

            ViewBag.sortieRondBetons = "active";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(SortieRondBeton sortieRondBeton)
        {
            if (ModelState.IsValid)
            {
                var y = _context.Add(sortieRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = y.Entity.Id });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieRondBeton.TypeDeTransportId);

            var maxNumBon = _context.SortieRondBetons.Max(x => x.NumBonA);
            var maxNumTicket = _context.SortieRondBetons.Max(x => x.NumTicket);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);
            ViewBag.NumTicket = _numTicketBonManagement.GenerateNextNum(maxNumTicket);
            ViewBag.sortieRondBetons = "active";
            return View(sortieRondBeton);
        }






        public async Task<IActionResult> Reprise(int? id)
        {

            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }

            var sortieRondBeton = await _context.SortieRondBetons.FindAsync(id);
            if (sortieRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieRondBeton.TypeDeTransportId);
            ViewBag.sortieRondBetons = "active";
            return View(sortieRondBeton);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reprise(int id, SortieRondBeton sortieRondBeton)
        {
            if (id != sortieRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieRondBeton);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieRondBetonExists(sortieRondBeton.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = sortieRondBeton.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieRondBeton.TypeDeTransportId);
            ViewBag.sortieRondBetons = "active";
            return View(sortieRondBeton);
        }






        public async Task<IActionResult> Modifier(int? id)
        {

            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }

            var sortieRondBeton = await _context.SortieRondBetons.FindAsync(id);
            if (sortieRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieRondBeton.TypeDeTransportId);
            ViewBag.sortieRondBetons = "active";
            return View(sortieRondBeton);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modifier(int id, SortieRondBeton sortieRondBeton)
        {
            if (id != sortieRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieRondBeton);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieRondBetonExists(sortieRondBeton.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = sortieRondBeton.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieRondBeton.TypeDeTransportId);
            ViewBag.sortieRondBetons = "active";
            return View(sortieRondBeton);
        }










        public async Task<IActionResult> ListFinished()
        {
            var applicationDbContext = _context.SortieRondBetons.Where(a => a.Termine == true).Include(a => a.Parc).Include(a => a.TypeDeCamion).Include(a => a.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetBon(int? id)
        {
            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }
            var sortieRondBeton = await _context.SortieRondBetons.FirstOrDefaultAsync(m => m.Id == id);

            if (sortieRondBeton == null)
            {
                return NotFound();
            }

            if (sortieRondBeton.Termine)
            {
                return RedirectToAction(nameof(BonReceptionSortieRondBeton), new { id = sortieRondBeton.Id });

            }
            else
            {
                return RedirectToAction(nameof(BonDechargement), new { id = sortieRondBeton.Id });

            }
        }


        public async Task<IActionResult> BonDechargement(int? id)
        {
            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }

            var sortieRondBeton = await _context.SortieRondBetons
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieRondBeton == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionSortieRondBeton.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");





            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


        public async Task<IActionResult> BonReceptionSortieRondBeton(int id)
        {
            if (id == null || _context.SortieRondBetons == null)
            {
                return NotFound();
            }

            var sortieRondBeton = await _context.SortieRondBetons
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieRondBeton == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionSortieRondBeton.rdlc";

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
