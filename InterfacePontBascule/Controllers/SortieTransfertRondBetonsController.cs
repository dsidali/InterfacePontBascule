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
    public class SortieTransfertRondBetonsController : Controller
    {
        private readonly ApplicationDbContext _context;


        private readonly IWebHostEnvironment _webHostEnvironment;
        private IComPortUsage _comPortUsage;
        private INumTicketBonManagement _numTicketBonManagement;

        public SortieTransfertRondBetonsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IComPortUsage comPortUsage, INumTicketBonManagement numTicketBonManagement)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _comPortUsage = comPortUsage;
            _numTicketBonManagement = numTicketBonManagement;
        }

        // GET: SortieTransfertRondBetons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SortieTransfertRondBetons.Where(a => a.Termine== false).Include(s => s.Parc).Include(s => s.TypeDeCamion).Include(s => s.TypeDeTransport);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SortieTransfertRondBetons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }

            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons
                .Include(s => s.Parc)
                .Include(s => s.TypeDeCamion)
                .Include(s => s.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";
            return View(sortieTransfertRondBeton);
        }

        // GET: SortieTransfertRondBetons/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";
            return View();
        }

        // POST: SortieTransfertRondBetons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBL,DateOp,Transporteur,Destination,TypeDeTransportId,TypeDeCamionId,Mat,Diametre,PCC,PCV,PQS,Observation,Termine")] SortieTransfertRondBeton sortieTransfertRondBeton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sortieTransfertRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieTransfertRondBeton.TypeDeTransportId);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";
            return View(sortieTransfertRondBeton);
        }

        // GET: SortieTransfertRondBetons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }

            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons.FindAsync(id);
            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieTransfertRondBeton.TypeDeTransportId);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";

            return View(sortieTransfertRondBeton);
        }

        // POST: SortieTransfertRondBetons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  SortieTransfertRondBeton sortieTransfertRondBeton)
        {
            if (id != sortieTransfertRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieTransfertRondBeton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieTransfertRondBetonExists(sortieTransfertRondBeton.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", sortieTransfertRondBeton.TypeDeTransportId);

            ViewBag.trsfopen = "menu-open";
            ViewBag.trsf = "active";
            ViewBag.trsfrb = "active";

            return View(sortieTransfertRondBeton);
        }

        // GET: SortieTransfertRondBetons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }

            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons
                .Include(s => s.Parc)
                .Include(s => s.TypeDeCamion)
                .Include(s => s.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }

            return View(sortieTransfertRondBeton);
        }

        // POST: SortieTransfertRondBetons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SortieTransfertRondBetons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SortieTransfertRondBetons'  is null.");
            }
            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons.FindAsync(id);
            if (sortieTransfertRondBeton != null)
            {
                _context.SortieTransfertRondBetons.Remove(sortieTransfertRondBeton);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SortieTransfertRondBetonExists(int id)
        {
          return (_context.SortieTransfertRondBetons?.Any(e => e.Id == id)).GetValueOrDefault();
        }











        public async Task<IActionResult> New()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport");


            ViewBag.e = User.Identity.Name;

            var maxNumBon = _context.SortieTransfertRondBetons.Max(x => x.NumBL);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);

            ViewBag.sortieTransfertRondBetons = "active";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(SortieTransfertRondBeton sortieTransfertRondBeton)
        {
            if (ModelState.IsValid)
            {
                var y = _context.Add(sortieTransfertRondBeton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = y.Entity.Id });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieTransfertRondBeton.TypeDeTransportId);

            var maxNumBon = _context.SortieTransfertRondBetons.Max(x => x.NumBL);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);
            ViewBag.sortieTransfertRondBetons = "active";
            return View(sortieTransfertRondBeton);
        }






        public async Task<IActionResult> Reprise(int? id)
        {

            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }

            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons.FindAsync(id);
            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieTransfertRondBeton.TypeDeTransportId);
            ViewBag.sortieTransfertRondBetons = "active";
            return View(sortieTransfertRondBeton);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reprise(int id, SortieTransfertRondBeton sortieTransfertRondBeton)
        {
            if (id != sortieTransfertRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieTransfertRondBeton);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieTransfertRondBetonExists(sortieTransfertRondBeton.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = sortieTransfertRondBeton.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieTransfertRondBeton.TypeDeTransportId);
            ViewBag.sortieTransfertRondBetons = "active";
            return View(sortieTransfertRondBeton);
        }





        public async Task<IActionResult> Modifier(int? id)
        {

            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }

            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons.FindAsync(id);
            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieTransfertRondBeton.TypeDeTransportId);
            ViewBag.sortieTransfertRondBetons = "active";
            return View(sortieTransfertRondBeton);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modifier(int id, SortieTransfertRondBeton sortieTransfertRondBeton)
        {
            if (id != sortieTransfertRondBeton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sortieTransfertRondBeton);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SortieTransfertRondBetonExists(sortieTransfertRondBeton.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = sortieTransfertRondBeton.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", sortieTransfertRondBeton.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", sortieTransfertRondBeton.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", sortieTransfertRondBeton.TypeDeTransportId);
            ViewBag.sortieTransfertRondBetons = "active";
            return View(sortieTransfertRondBeton);
        }


        public async Task<IActionResult> ListFinished()
        {
            var applicationDbContext = _context.SortieTransfertRondBetons.Where(a => a.Termine == true).Include(a => a.Parc).Include(a => a.TypeDeCamion).Include(a => a.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetBon(int? id)
        {
            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }
            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons.FirstOrDefaultAsync(m => m.Id == id);

            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }

            if (sortieTransfertRondBeton.Termine)
            {
                return RedirectToAction(nameof(BonReceptionSortieTransfertRondBeton), new { id = sortieTransfertRondBeton.Id });

            }
            else
            {
                return RedirectToAction(nameof(BonDechargement), new { id = sortieTransfertRondBeton.Id });

            }
        }


        public async Task<IActionResult> BonDechargement(int? id)
        {
            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }

            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportTransfert.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");

            parameters.Add("type", "chargement");
            parameters.Add("NumTicket", sortieTransfertRondBeton.NumBL);
            parameters.Add("Numero", sortieTransfertRondBeton.NumBL);
            parameters.Add("Nom", sortieTransfertRondBeton.Transporteur);
            parameters.Add("Date", sortieTransfertRondBeton.DateOp.ToShortDateString());
            parameters.Add("Heure", sortieTransfertRondBeton.DateOp.TimeOfDay.ToString());
            parameters.Add("Brut", sortieTransfertRondBeton.PCC.ToString());
            parameters.Add("Tar", sortieTransfertRondBeton.PCV.ToString());
            parameters.Add("Net", sortieTransfertRondBeton.PQS.ToString());
            parameters.Add("Netrecu", sortieTransfertRondBeton.PQS.ToString());
            parameters.Add("Categorie", "Ronds à beton");
            parameters.Add("Observation", sortieTransfertRondBeton.Observation);
            parameters.Add("ParcId", sortieTransfertRondBeton.Parc.Id.ToString());
            parameters.Add("TypeTransport", sortieTransfertRondBeton.TypeDeTransport.TypeTransport);
            parameters.Add("User", User.Identity.Name);
            parameters.Add("Matricule", sortieTransfertRondBeton.Mat);
            parameters.Add("DechetOrDiamatre", sortieTransfertRondBeton.Diametre.ToString());



            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


        public async Task<IActionResult> BonReceptionSortieTransfertRondBeton(int id)
        {
            if (id == null || _context.SortieTransfertRondBetons == null)
            {
                return NotFound();
            }

            var sortieTransfertRondBeton = await _context.SortieTransfertRondBetons
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sortieTransfertRondBeton == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportTransfert.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");

            parameters.Add("type", "sortie");
            parameters.Add("NumTicket", sortieTransfertRondBeton.NumBL);
            parameters.Add("Numero", sortieTransfertRondBeton.NumBL);
            parameters.Add("Nom", sortieTransfertRondBeton.Transporteur);
            parameters.Add("Date", sortieTransfertRondBeton.DateOp.ToShortDateString());
            parameters.Add("Heure", sortieTransfertRondBeton.DateOp.TimeOfDay.ToString());
            parameters.Add("Brut", sortieTransfertRondBeton.PCC.ToString());
            parameters.Add("Tar", sortieTransfertRondBeton.PCV.ToString());
            parameters.Add("Net", sortieTransfertRondBeton.PQS.ToString());
            parameters.Add("Netrecu", sortieTransfertRondBeton.PQS.ToString());
            parameters.Add("Categorie", "Ronds à beton");
            parameters.Add("Observation", sortieTransfertRondBeton.Observation);
            parameters.Add("ParcId", sortieTransfertRondBeton.Parc.Id.ToString());
            parameters.Add("TypeTransport", sortieTransfertRondBeton.TypeDeTransport.TypeTransport);
            parameters.Add("User", User.Identity.Name);
            parameters.Add("Matricule", sortieTransfertRondBeton.Mat);
            parameters.Add("DechetOrDiamatre", sortieTransfertRondBeton.Diametre.ToString());


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
