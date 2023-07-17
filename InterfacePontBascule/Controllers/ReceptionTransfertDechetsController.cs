using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterfacePontBascule.Data;
using InterfacePontBascule.Models;
using InterfacePontBascule.Business;

namespace InterfacePontBascule.Controllers
{
    public class ReceptionTransfertDechetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private IComPortUsage _comPortUsage;
        private INumTicketBonManagement _numTicketBonManagement;
        public ReceptionTransfertDechetsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IComPortUsage comPortUsage, INumTicketBonManagement numTicketBonManagement)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _comPortUsage = comPortUsage;
            _numTicketBonManagement = numTicketBonManagement;
        }

        // GET: ReceptionTransfertDechets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReceptionTransfertDechets.Where(a => a.Termine==false).Include(r => r.Parc).Include(r => r.TypeDeCamion).Include(r => r.TypeDeDechet).Include(r => r.TypeDeTransport);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfdech = "active";

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReceptionTransfertDechets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }

            var receptionTransfertDechet = await _context.ReceptionTransfertDechets
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeDechet)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }
            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfdech = "active";
            return View(receptionTransfertDechet);
        }

        // GET: ReceptionTransfertDechets/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");
            return View();
        }

        // POST: ReceptionTransfertDechets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBL,DateOp,Transporteur,Provenance,TypeDeTransportId,TypeDeCamionId,Mat,TypeDeDechetId,PCC,PCV,PQS,Observation,Termine")] ReceptionTransfertDechet receptionTransfertDechet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receptionTransfertDechet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertDechet.TypeDeTransportId);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfdech = "active";


            return View(receptionTransfertDechet);
        }

        // GET: ReceptionTransfertDechets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }

            var receptionTransfertDechet = await _context.ReceptionTransfertDechets.FindAsync(id);
            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertDechet.TypeDeTransportId);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfdech = "active";

            return View(receptionTransfertDechet);
        }

        // POST: ReceptionTransfertDechets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBL,DateOp,Transporteur,Provenance,TypeDeTransportId,TypeDeCamionId,Mat,TypeDeDechetId,PCC,PCV,PQS,Observation,Termine")] ReceptionTransfertDechet receptionTransfertDechet)
        {
            if (id != receptionTransfertDechet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionTransfertDechet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionTransfertDechetExists(receptionTransfertDechet.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", receptionTransfertDechet.TypeDeTransportId);

            ViewBag.rectrsfopen = "menu-open";
            ViewBag.rectrsf = "active";
            ViewBag.rectrsfdech = "active";

            return View(receptionTransfertDechet);
        }

        // GET: ReceptionTransfertDechets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }

            var receptionTransfertDechet = await _context.ReceptionTransfertDechets
                .Include(r => r.Parc)
                .Include(r => r.TypeDeCamion)
                .Include(r => r.TypeDeDechet)
                .Include(r => r.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }

            return View(receptionTransfertDechet);
        }

        // POST: ReceptionTransfertDechets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReceptionTransfertDechets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReceptionTransfertDechets'  is null.");
            }
            var receptionTransfertDechet = await _context.ReceptionTransfertDechets.FindAsync(id);
            if (receptionTransfertDechet != null)
            {
                _context.ReceptionTransfertDechets.Remove(receptionTransfertDechet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionTransfertDechetExists(int id)
        {
          return (_context.ReceptionTransfertDechets?.Any(e => e.Id == id)).GetValueOrDefault();
        }











        public async Task<IActionResult> New()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport");


            ViewBag.e = User.Identity.Name;

            var maxNumBon = _context.ReceptionTransfertDechets.Max(x => x.NumBL);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);

            ViewBag.receptionTransfertDechets = "active";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ReceptionTransfertDechet receptionTransfertDechet)
        {
            if (ModelState.IsValid)
            {
                var y = _context.Add(receptionTransfertDechet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = y.Entity.Id });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionTransfertDechet.TypeDeTransportId);

            var maxNumBon = _context.ReceptionTransfertDechets.Max(x => x.NumBL);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);
            ViewBag.receptionTransfertDechets = "active";
            return View(receptionTransfertDechet);
        }






        public async Task<IActionResult> Reprise(int? id)
        {

            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }

            var receptionTransfertDechet = await _context.ReceptionTransfertDechets.FindAsync(id);
            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionTransfertDechet.TypeDeTransportId);
            ViewBag.receptionTransfertDechets = "active";
            return View(receptionTransfertDechet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reprise(int id, ReceptionTransfertDechet receptionTransfertDechet)
        {
            if (id != receptionTransfertDechet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionTransfertDechet);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionTransfertDechetExists(receptionTransfertDechet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = receptionTransfertDechet.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionTransfertDechet.TypeDeTransportId);
            ViewBag.receptionTransfertDechets = "active";
            return View(receptionTransfertDechet);
        }









        public async Task<IActionResult> Modifier(int? id)
        {

            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }

            var receptionTransfertDechet = await _context.ReceptionTransfertDechets.FindAsync(id);
            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionTransfertDechet.TypeDeTransportId);
            ViewBag.receptionTransfertDechets = "active";
            return View(receptionTransfertDechet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modifier(int id, ReceptionTransfertDechet receptionTransfertDechet)
        {
            if (id != receptionTransfertDechet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionTransfertDechet);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionTransfertDechetExists(receptionTransfertDechet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = receptionTransfertDechet.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", receptionTransfertDechet.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", receptionTransfertDechet.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet", receptionTransfertDechet.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", receptionTransfertDechet.TypeDeTransportId);
            ViewBag.receptionTransfertDechets = "active";
            return View(receptionTransfertDechet);
        }
















        public async Task<IActionResult> ListFinished()
        {
            var applicationDbContext = _context.ReceptionTransfertDechets.Where(a => a.Termine == true).Include(a => a.Parc).Include(a => a.TypeDeCamion).Include(a => a.TypeDeDechet).Include(a => a.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetBon(int? id)
        {
            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }
            var receptionTransfertDechet = await _context.ReceptionTransfertDechets.FirstOrDefaultAsync(m => m.Id == id);

            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }

            if (receptionTransfertDechet.Termine)
            {
                return RedirectToAction(nameof(BonReceptionReceptionTransfertDechet), new { id = receptionTransfertDechet.Id });

            }
            else
            {
                return RedirectToAction(nameof(BonDechargement), new { id = receptionTransfertDechet.Id });

            }
        }


        public async Task<IActionResult> BonDechargement(int? id)
        {
            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }

            var receptionTransfertDechet = await _context.ReceptionTransfertDechets
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeDechet)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportTransfert.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");


            parameters.Add("type", "dechargement");
            parameters.Add("NumTicket", receptionTransfertDechet.NumBL);
            parameters.Add("Numero", receptionTransfertDechet.NumBL);
            parameters.Add("Nom", receptionTransfertDechet.Transporteur);
            parameters.Add("Date", receptionTransfertDechet.DateOp.ToShortDateString());
            parameters.Add("Heure", receptionTransfertDechet.DateOp.TimeOfDay.ToString());
            parameters.Add("Brut", receptionTransfertDechet.PCC.ToString());
            parameters.Add("Tar", receptionTransfertDechet.PCV.ToString());
            parameters.Add("Net", receptionTransfertDechet.PQS.ToString());
            parameters.Add("Netrecu", receptionTransfertDechet.PQS.ToString());
            parameters.Add("Categorie", "Dechets ferreux");
            parameters.Add("Observation", receptionTransfertDechet.Observation);
            parameters.Add("ParcId", receptionTransfertDechet.Parc.Id.ToString());
            parameters.Add("TypeTransport", receptionTransfertDechet.TypeDeTransport.TypeTransport);
            parameters.Add("User", User.Identity.Name);
            parameters.Add("Matricule", receptionTransfertDechet.Mat);
            parameters.Add("DechetOrDiamatre", receptionTransfertDechet.TypeDeDechet.TypeDechet);


            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


        public async Task<IActionResult> BonReceptionReceptionTransfertDechet(int id)
        {
            if (id == null || _context.ReceptionTransfertDechets == null)
            {
                return NotFound();
            }

            var receptionTransfertDechet = await _context.ReceptionTransfertDechets
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeDechet)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionTransfertDechet == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportTransfert.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");

            parameters.Add("type", "Reception");
            parameters.Add("NumTicket", receptionTransfertDechet.NumBL);
            parameters.Add("Numero", receptionTransfertDechet.NumBL);
            parameters.Add("Nom", receptionTransfertDechet.Transporteur);
            parameters.Add("Date", receptionTransfertDechet.DateOp.ToShortDateString());
            parameters.Add("Heure", receptionTransfertDechet.DateOp.TimeOfDay.ToString());
            parameters.Add("Brut", receptionTransfertDechet.PCC.ToString());
            parameters.Add("Tar", receptionTransfertDechet.PCV.ToString());
            parameters.Add("Net", receptionTransfertDechet.PQS.ToString());
            parameters.Add("Netrecu", receptionTransfertDechet.PQS.ToString());
            parameters.Add("Categorie", "Dechets ferreux");
            parameters.Add("Observation", receptionTransfertDechet.Observation);
            parameters.Add("ParcId", receptionTransfertDechet.Parc.Id.ToString());
            parameters.Add("TypeTransport", receptionTransfertDechet.TypeDeTransport.TypeTransport);
            parameters.Add("User", User.Identity.Name);
            parameters.Add("Matricule", receptionTransfertDechet.Mat);
            parameters.Add("DechetOrDiamatre", receptionTransfertDechet.TypeDeDechet.TypeDechet);


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
