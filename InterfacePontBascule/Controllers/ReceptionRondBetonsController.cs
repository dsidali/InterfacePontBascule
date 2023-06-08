using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterfacePontBascule.Data;
using InterfacePontBascule.Models;
using AspNetCore.Reporting;
using InterfacePontBascule.Business;

namespace InterfacePontBascule.Controllers
{
    public class ReceptionRondBetonsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        // private readonly ILogger _logger;
        private IComPortUsage _comPortUsage;
        private INumTicketBonManagement _numTicketBonManagement;

        public ReceptionRondBetonsController(ApplicationDbContext context, IComPortUsage comPortUsage, IWebHostEnvironment webHostEnvironment, INumTicketBonManagement numTicketBonManagement)
        {
            _context = context;
            _comPortUsage = comPortUsage;
            _webHostEnvironment = webHostEnvironment;
            _numTicketBonManagement = numTicketBonManagement;
        }

        // GET: ReceptionRondBetons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReceptionRondBetons.Where(a => a.Termine == false).Include(r => r.Parc).Include(r => r.TypeDeCamion).Include(r => r.TypeDeTransport);

            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbrcpt = "active";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReceptionRondBetons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReceptionRondBetons == null)
            {
                return NotFound();
            }

            var receptionRondBeton = await _context.ReceptionRondBetons
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionRondBeton == null)
            {
                return NotFound();
            }
            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbrcpt = "active";
            return View(receptionRondBeton);
        }

        // GET: ReceptionRondBetons/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");

            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbrcpt = "active";

            return View();
        }

        // POST: ReceptionRondBetons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBonA,NumTicket,DateOp,Source,Transporteur,Mat,TypeDeTransportId,TypeDeCamionId,Diametre,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] ReceptionRondBeton receptionRondBeton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receptionRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionRondBeton.TypeDeTransportId);

            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbrcpt = "active";
            return View(receptionRondBeton);
        }

        // GET: ReceptionRondBetons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReceptionRondBetons == null)
            {
                return NotFound();
            }

            var receptionRondBeton = await _context.ReceptionRondBetons.FindAsync(id);
            if (receptionRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionRondBeton.TypeDeTransportId);

            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbrcpt = "active";

            return View(receptionRondBeton);
        }

        // POST: ReceptionRondBetons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBonA,NumTicket,DateOp,Source,Transporteur,Mat,TypeDeTransportId,TypeDeCamionId,Diametre,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] ReceptionRondBeton receptionRondBeton)
        {
            if (id != receptionRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionRondBeton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionRondBetonExists(receptionRondBeton.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionRondBeton.TypeDeTransportId);

            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbrcpt = "active";

            return View(receptionRondBeton);
        }

        // GET: ReceptionRondBetons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReceptionRondBetons == null)
            {
                return NotFound();
            }

            var receptionRondBeton = await _context.ReceptionRondBetons
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionRondBeton == null)
            {
                return NotFound();
            }
            ViewBag.rbopen = "menu-open";
            ViewBag.rb = "active";
            ViewBag.rbrcpt = "active";
            return View(receptionRondBeton);
        }

        // POST: ReceptionRondBetons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReceptionRondBetons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReceptionRondBetons'  is null.");
            }
            var receptionRondBeton = await _context.ReceptionRondBetons.FindAsync(id);
            if (receptionRondBeton != null)
            {
                _context.ReceptionRondBetons.Remove(receptionRondBeton);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionRondBetonExists(int id)
        {
          return (_context.ReceptionRondBetons?.Any(e => e.Id == id)).GetValueOrDefault();
        }




        /*********************************************************************************************************************************************/

        public async Task<IActionResult> New()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport");


            ViewBag.e = User.Identity.Name;

            var maxNumBon = _context.ReceptionRondBetons.Max(x => x.NumBonA);
            var maxNumTicket = _context.ReceptionRondBetons.Max(x => x.NumTicket);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);
            ViewBag.NumTicket = _numTicketBonManagement.GenerateNextNum(maxNumTicket);

            ViewBag.receptionRondBetons = "active";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ReceptionRondBeton receptionRondBeton)
        {
            if (ModelState.IsValid)
            {
                var y = _context.Add(receptionRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = y.Entity.Id });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionRondBeton.TypeDeTransportId);

            var maxNumBon = _context.ReceptionRondBetons.Max(x => x.NumBonA);
            var maxNumTicket = _context.ReceptionRondBetons.Max(x => x.NumTicket);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);
            ViewBag.NumTicket = _numTicketBonManagement.GenerateNextNum(maxNumTicket);
            ViewBag.receptionRondBetons = "active";
            return View(receptionRondBeton);
        }

        




        public async Task<IActionResult> Reprise(int? id)
        {

            if (id == null || _context.ReceptionRondBetons == null)
            {
                return NotFound();
            }

            var receptionRondBeton = await _context.ReceptionRondBetons.FindAsync(id);
            if (receptionRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionRondBeton.TypeDeTransportId);
            ViewBag.receptionRondBetons = "active";
            return View(receptionRondBeton);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reprise(int id, ReceptionRondBeton receptionRondBeton)
        {
            if (id != receptionRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionRondBeton);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionRondBetonExists(receptionRondBeton.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = receptionRondBeton.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionRondBeton.TypeDeTransportId);
            ViewBag.receptionRondBetons = "active";
            return View(receptionRondBeton);
        }



        public async Task<IActionResult> ListFinished()
        {
            var applicationDbContext = _context.ReceptionRondBetons.Where(a => a.Termine == true).Include(a => a.Parc).Include(a => a.TypeDeCamion).Include(a => a.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetBon(int? id)
        {
            if (id == null || _context.ReceptionRondBetons == null)
            {
                return NotFound();
            }
            var receptionRondBeton = await _context.ReceptionRondBetons.FirstOrDefaultAsync(m => m.Id == id);

            if (receptionRondBeton == null)
            {
                return NotFound();
            }

            if (receptionRondBeton.Termine)
            {
                return RedirectToAction(nameof(BonReceptionReceptionRondBeton), new { id = receptionRondBeton.Id });

            }
            else
            {
                return RedirectToAction(nameof(BonDechargement), new { id = receptionRondBeton.Id });

            }
        }


        public async Task<IActionResult> BonDechargement(int? id)
        {
            if (id == null || _context.ReceptionRondBetons == null)
            {
                return NotFound();
            }

            var receptionRondBeton = await _context.ReceptionRondBetons
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionRondBeton == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionReceptionRondBeton.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");



         

            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


        public async Task<IActionResult> BonReceptionReceptionRondBeton(int id)
        {
            if (id == null || _context.ReceptionRondBetons == null)
            {
                return NotFound();
            }

            var receptionRondBeton = await _context.ReceptionRondBetons
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionRondBeton == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionReceptionRondBeton.rdlc";

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
