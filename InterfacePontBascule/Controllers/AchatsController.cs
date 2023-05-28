using AspNetCore.Reporting;
using AspNetCore.Reporting.ReportExecutionService;
using InterfacePontBascule.Data;
using InterfacePontBascule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using InterfacePontBascule.Business;

namespace InterfacePontBascule.Controllers
{
    //  [Authorize]
    public class AchatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
       // private readonly ILogger _logger;
        private  IComPortUsage _comPortUsage;
        public AchatsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IComPortUsage comPortUsage)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
          //  _logger = logger;
            _comPortUsage = comPortUsage;
        }

        // GET: Achats
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Achats.Where(a => a.Termine == false).Include(a => a.Parc).Include(a => a.TypeDeCamion).Include(a => a.TypeDeDechet).Include(a => a.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Achats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Achats == null)
            {
                return NotFound();
            }

            var achat = await _context.Achats
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeDechet)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achat == null)
            {
                return NotFound();
            }

            return View(achat);
        }

        // GET: Achats/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport");
            ViewBag.e = User.Identity.Name;
            return View();
        }

        // POST: Achats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBonA,NumTicket,Mat,Transporteur,Source,TypeDeTransportId,TypeDeCamionId,TypeDeDechetId,DateOP,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] Achat achat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(achat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", achat.TypeDeTransportId);
            return View(achat);
        }

        // GET: Achats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Achats == null)
            {
                return NotFound();
            }

            var achat = await _context.Achats.FindAsync(id);
            if (achat == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", achat.TypeDeTransportId);
            return View(achat);
        }

        // POST: Achats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParcId,NumBonA,NumTicket,Mat,Transporteur,Source,TypeDeTransportId,TypeDeCamionId,TypeDeDechetId,DateOP,PCC,PCV,PB,PQRa,PQS,Observation,Termine")] Achat achat)
        {
            if (id != achat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchatExists(achat.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", achat.TypeDeTransportId);
            return View(achat);
        }

        // GET: Achats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Achats == null)
            {
                return NotFound();
            }

            var achat = await _context.Achats
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeDechet)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achat == null)
            {
                return NotFound();
            }

            return View(achat);
        }

        // POST: Achats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Achats == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Achats'  is null.");
            }
            var achat = await _context.Achats.FindAsync(id);
            if (achat != null)
            {
                _context.Achats.Remove(achat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AchatExists(int id)
        {
            return (_context.Achats?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /*********************************************************************************************************************************************/

        public async Task<IActionResult> New()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Achat achat)
        {
            if (ModelState.IsValid)
            {
                var y = _context.Add(achat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = y.Entity.Id });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", achat.TypeDeTransportId);
            return View(achat);
        }


        public async Task<IActionResult> Modifier(int? id)
        {

            if (id == null || _context.Achats == null)
            {
                return NotFound();
            }

            var achat = await _context.Achats.FindAsync(id);
            if (achat == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", achat.TypeDeTransportId);
            return View(achat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modifier(int id,  Achat achat)
        {
            if (id != achat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchatExists(achat.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", achat.TypeDeTransportId);
            return View(achat);
        }




        public async Task<IActionResult> Reprise(int? id)
        {

            if (id == null || _context.Achats == null)
            {
                return NotFound();
            }

            var achat = await _context.Achats.FindAsync(id);
            if (achat == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", achat.TypeDeTransportId);
            return View(achat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reprise(int id,  Achat achat)
        {
            if (id != achat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchatExists(achat.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", achat.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", achat.TypeDeCamionId);
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "Id", achat.TypeDeDechetId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", achat.TypeDeTransportId);
            return View(achat);
        }



        public async Task<IActionResult> ListFinished()
        {
            var applicationDbContext = _context.Achats.Where(a => a.Termine == true).Include(a => a.Parc).Include(a => a.TypeDeCamion).Include(a => a.TypeDeDechet).Include(a => a.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }



        public async Task<IActionResult> BonDechargement(int? id)
        {
            if (id == null || _context.Achats == null)
            {
                return NotFound();
            }

            var achat = await _context.Achats
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeDechet)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achat == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionAchat.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
          //  parameters.Add("Id", "Welcome");



            parameters.Add("TypeOp", "Dechargement");
            parameters.Add("Numero", achat.NumBonA);
            parameters.Add("Nom", achat.Transporteur);
            parameters.Add("Date", achat.DateOP.ToShortDateString());
            parameters.Add("Heure", achat.DateOP.TimeOfDay.ToString());
            parameters.Add("NumTicket", achat.NumTicket);
            parameters.Add("Brut", achat.PCC.ToString());
            parameters.Add("Tar", achat.PCV.ToString());
            parameters.Add("Net", achat.PB.ToString());
            parameters.Add("Rabais", achat.PQRa.ToString());
            parameters.Add("Netrecu", achat.PQS.ToString());
            parameters.Add("Categorie", "Dechets ferreux");
            parameters.Add("Observation", achat.Observation);
            parameters.Add("ParcId", achat.Parc.Id.ToString());
            parameters.Add("TypeTransport", achat.TypeDeTransport.TypeTransport);
            parameters.Add("User", User.Identity.Name);
            parameters.Add("Matricule", achat.Mat);
            parameters.Add("TypeDechets",achat.TypeDeDechet.TypeDechet);

            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


        public async Task<IActionResult> BonReceptionAchat(int id)
        {
            if (id == null || _context.Achats == null)
            {
                return NotFound();
            }

            var achat = await _context.Achats
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeDechet)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achat == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionAchat.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");



            parameters.Add("TypeOp", "Reception");
            parameters.Add("Numero", achat.NumBonA);
            parameters.Add("Nom", achat.Transporteur);
            parameters.Add("Date", achat.DateOP.ToShortDateString());
            parameters.Add("Heure", achat.DateOP.TimeOfDay.ToString());
            parameters.Add("NumTicket", achat.NumTicket);
            parameters.Add("Brut", achat.PCC.ToString());
            parameters.Add("Tar", achat.PCV.ToString());
            parameters.Add("Net", achat.PB.ToString());
            parameters.Add("Rabais", achat.PQRa.ToString());
            parameters.Add("Netrecu", achat.PQS.ToString());
            parameters.Add("Categorie", "Dechets ferreux");
            parameters.Add("Observation", achat.Observation);
            parameters.Add("ParcId", achat.Parc.Id.ToString());
            parameters.Add("TypeTransport", achat.TypeDeTransport.TypeTransport);
            parameters.Add("User", User.Identity.Name);
            parameters.Add("Matricule", achat.Mat);
            parameters.Add("TypeDechets", achat.TypeDeDechet.TypeDechet);

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
