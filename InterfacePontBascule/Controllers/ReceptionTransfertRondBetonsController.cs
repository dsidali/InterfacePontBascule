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
    public class ReceptionTransfertRondBetonsController : Controller
    {
        private readonly ApplicationDbContext _context;


        private readonly IWebHostEnvironment _webHostEnvironment;
        private IComPortUsage _comPortUsage;
        private INumTicketBonManagement _numTicketBonManagement;
        public ReceptionTransfertRondBetonsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IComPortUsage comPortUsage, INumTicketBonManagement numTicketBonManagement)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _comPortUsage = comPortUsage;
            _numTicketBonManagement = numTicketBonManagement;
        }

        // GET: ReceptionTransfertRondBetons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReceptionTransfertRondBetons.Where( a=> a.Termine==false).Include(r => r.Parc).Include(r => r.TypeDeCamion).Include(r => r.TypeDeTransport);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfrb = "active";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReceptionTransfertRondBetons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }

            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfrb = "active";
            return View(receptionTransfertRondBeton);
        }

        // GET: ReceptionTransfertRondBetons/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfrb = "active";

            return View();
        }

        // POST: ReceptionTransfertRondBetons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReceptionTransfertRondBeton receptionTransfertRondBeton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receptionTransfertRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertRondBeton.TypeDeTransportId);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfrb = "active";

            return View(receptionTransfertRondBeton);
        }

        // GET: ReceptionTransfertRondBetons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }

            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons.FindAsync(id);
            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertRondBeton.TypeDeTransportId);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfrb = "active";

            return View(receptionTransfertRondBeton);
        }

        // POST: ReceptionTransfertRondBetons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBL,DateOp,Transporteur,Provenance,TypeDeTransportId,TypeDeCamionId,Mat,Diametre,PCC,PCV,PQS,Observation,Termine")] ReceptionTransfertRondBeton receptionTransfertRondBeton)
        {
            if (id != receptionTransfertRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionTransfertRondBeton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionTransfertRondBetonExists(receptionTransfertRondBeton.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertRondBeton.TypeDeTransportId);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfrb = "active";

            return View(receptionTransfertRondBeton);
        }

        // GET: ReceptionTransfertRondBetons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }

            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfrb = "active";
            return View(receptionTransfertRondBeton);
        }

        // POST: ReceptionTransfertRondBetons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReceptionTransfertRondBetons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReceptionTransfertRondBetons'  is null.");
            }
            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons.FindAsync(id);
            if (receptionTransfertRondBeton != null)
            {
                _context.ReceptionTransfertRondBetons.Remove(receptionTransfertRondBeton);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionTransfertRondBetonExists(int id)
        {
          return (_context.ReceptionTransfertRondBetons?.Any(e => e.Id == id)).GetValueOrDefault();
        }










        public async Task<IActionResult> New()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport");


            ViewBag.e = User.Identity.Name;

            var maxNumBon = _context.ReceptionTransfertRondBetons.Max(x => x.NumBL);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);

            ViewBag.receptionTransfertRondBetons = "active";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ReceptionTransfertRondBeton receptionTransfertRondBeton)
        {
            if (ModelState.IsValid)
            {
                var y = _context.Add(receptionTransfertRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = y.Entity.Id });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionTransfertRondBeton.TypeDeTransportId);

            var maxNumBon = _context.ReceptionTransfertRondBetons.Max(x => x.NumBL);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);
            ViewBag.receptionTransfertRondBetons = "active";
            return View(receptionTransfertRondBeton);
        }






        public async Task<IActionResult> Reprise(int? id)
        {

            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }

            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons.FindAsync(id);
            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionTransfertRondBeton.TypeDeTransportId);
            ViewBag.receptionTransfertRondBetons = "active";
            return View(receptionTransfertRondBeton);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reprise(int id, ReceptionTransfertRondBeton receptionTransfertRondBeton)
        {
            if (id != receptionTransfertRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionTransfertRondBeton);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionTransfertRondBetonExists(receptionTransfertRondBeton.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = receptionTransfertRondBeton.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionTransfertRondBeton.TypeDeTransportId);
            ViewBag.receptionTransfertRondBetons = "active";
            return View(receptionTransfertRondBeton);
        }












        public async Task<IActionResult> Modifier(int? id)
        {

            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }

            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons.FindAsync(id);
            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionTransfertRondBeton.TypeDeTransportId);
            ViewBag.receptionTransfertRondBetons = "active";
            return View(receptionTransfertRondBeton);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modifier(int id, ReceptionTransfertRondBeton receptionTransfertRondBeton)
        {
            if (id != receptionTransfertRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionTransfertRondBeton);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionTransfertRondBetonExists(receptionTransfertRondBeton.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = receptionTransfertRondBeton.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionTransfertRondBeton.TypeDeTransportId);
            ViewBag.receptionTransfertRondBetons = "active";
            return View(receptionTransfertRondBeton);
        }





        public async Task<IActionResult> ListFinished()
        {
            var applicationDbContext = _context.ReceptionTransfertRondBetons.Where(a => a.Termine == true).Include(a => a.Parc).Include(a => a.TypeDeCamion).Include(a => a.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetBon(int? id)
        {
            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }
            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons.FirstOrDefaultAsync(m => m.Id == id);

            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }

            if (receptionTransfertRondBeton.Termine)
            {
                return RedirectToAction(nameof(BonReceptionReceptionTransfertRondBeton), new { id = receptionTransfertRondBeton.Id });

            }
            else
            {
                return RedirectToAction(nameof(BonDechargement), new { id = receptionTransfertRondBeton.Id });

            }
        }


        public async Task<IActionResult> BonDechargement(int? id)
        {
            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }

            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionReceptionTransfertRondBeton.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");





            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


        public async Task<IActionResult> BonReceptionReceptionTransfertRondBeton(int id)
        {
            if (id == null || _context.ReceptionTransfertRondBetons == null)
            {
                return NotFound();
            }

            var receptionTransfertRondBeton = await _context.ReceptionTransfertRondBetons
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertRondBeton == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionReceptionTransfertRondBeton.rdlc";

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
