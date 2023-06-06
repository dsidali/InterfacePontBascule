using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterfacePontBascule.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InterfacePontBascule.Data;
using InterfacePontBascule.Models;
using AspNetCore.Reporting;

namespace InterfacePontBascule.Controllers
{
    public class PesagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        // private readonly ILogger _logger;
        private IComPortUsage _comPortUsage;
        private INumTicketBonManagement _numTicketBonManagement;
        public PesagesController(ApplicationDbContext context, IComPortUsage comPortUsage, INumTicketBonManagement numTicketBonManagement, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _comPortUsage = comPortUsage;
            _numTicketBonManagement = numTicketBonManagement;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pesages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pesages.Where(a => a.Termine == false).Include(p => p.Parc).Include(p => p.TypeDeCamion).Include(p => p.TypeDeTransport);

            ViewBag.pesage = "active";
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pesages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }

            var pesage = await _context.Pesages
                .Include(p => p.Parc)
                .Include(p => p.TypeDeCamion)
                .Include(p => p.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pesage == null)
            {
                return NotFound();
            }
            ViewBag.pesage = "active";
            return View(pesage);
        }

        // GET: Pesages/Create
        public IActionResult Create()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id");
            ViewBag.pesage = "active";
            return View();
        }

        // POST: Pesages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParcId,NumBonA,NumTicket,Transporteur,TypeDeTransportId,TypeDeCamionId,Mat,DateOP,PCC,PCV,QP,Observation,Termine")] Pesage pesage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pesage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", pesage.TypeDeTransportId);
            ViewBag.pesage = "active";
            return View(pesage);
        }

        // GET: Pesages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }

            var pesage = await _context.Pesages.FindAsync(id);
            if (pesage == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", pesage.TypeDeTransportId);
            ViewBag.pesage = "active";
            return View(pesage);
        }

        // POST: Pesages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pesage pesage)
        {
            if (id != pesage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pesage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PesageExists(pesage.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", pesage.TypeDeTransportId);
            ViewBag.pesage = "active";
            return View(pesage);
        }

        // GET: Pesages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }

            var pesage = await _context.Pesages
                .Include(p => p.Parc)
                .Include(p => p.TypeDeCamion)
                .Include(p => p.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pesage == null)
            {
                return NotFound();
            }
            ViewBag.pesage = "active";
            return View(pesage);
        }

        // POST: Pesages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pesages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pesages'  is null.");
            }
            var pesage = await _context.Pesages.FindAsync(id);
            if (pesage != null)
            {
                _context.Pesages.Remove(pesage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PesageExists(int id)
        {
          return (_context.Pesages?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /********************************************************************/


        public async Task<IActionResult> New()
        {
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id");
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion");
            ViewData["TypeDeDechetId"] = new SelectList(_context.TypeDeDechets, "Id", "TypeDechet");
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport");


            ViewBag.e = User.Identity.Name;

            var maxNumBon = _context.Pesages.Max(x => x.NumBonA);
            var maxNumTicket = _context.Pesages.Max(x => x.NumTicket);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);
            ViewBag.NumTicket = _numTicketBonManagement.GenerateNextNum(maxNumTicket);

            ViewBag.pesages = "active";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Pesage pesage)
        {
            if (ModelState.IsValid)
            {
                var y = _context.Add(pesage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = y.Entity.Id });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", pesage.TypeDeTransportId);

            var maxNumBon = _context.Pesages.Max(x => x.NumBonA);
            var maxNumTicket = _context.Pesages.Max(x => x.NumTicket);
            ViewBag.NumBon = _numTicketBonManagement.GenerateNextNum(maxNumBon);
            ViewBag.NumTicket = _numTicketBonManagement.GenerateNextNum(maxNumTicket);
            ViewBag.pesages = "active";
            return View(pesage);
        }


        public async Task<IActionResult> Modifier(int? id)
        {

            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }

            var pesage = await _context.Pesages.FindAsync(id);
            if (pesage == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", pesage.TypeDeTransportId);
            ViewBag.pesages = "active";
            return View(pesage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modifier(int id, Pesage pesage)
        {
            if (id != pesage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pesage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PesageExists(pesage.Id))
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
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "Id", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "Id", pesage.TypeDeTransportId);

            ViewBag.pesages = "active";
            return View(pesage);
        }




        public async Task<IActionResult> Reprise(int? id)
        {

            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }

            var pesage = await _context.Pesages.FindAsync(id);
            if (pesage == null)
            {
                return NotFound();
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", pesage.TypeDeTransportId);
            ViewBag.pesages = "active";
            return View(pesage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reprise(int id, Pesage pesage)
        {
            if (id != pesage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pesage);
                    await _context.SaveChangesAsync();



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PesageExists(pesage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = pesage.Id });
            }
            ViewData["ParcId"] = new SelectList(_context.Parcs, "Id", "Id", pesage.ParcId);
            ViewData["TypeDeCamionId"] = new SelectList(_context.TypeDeCamions, "Id", "TypeCamion", pesage.TypeDeCamionId);
            ViewData["TypeDeTransportId"] = new SelectList(_context.TypeDeTransports, "Id", "TypeTransport", pesage.TypeDeTransportId);
            ViewBag.pesages = "active";
            return View(pesage);
        }



        public async Task<IActionResult> ListFinished()
        {
            var applicationDbContext = _context.Pesages.Where(a => a.Termine == true).Include(a => a.Parc).Include(a => a.TypeDeCamion).Include(a => a.TypeDeTransport);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> GetBon(int? id)
        {
            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }
            var pesage = await _context.Pesages.FirstOrDefaultAsync(m => m.Id == id);

            if (pesage == null)
            {
                return NotFound();
            }

            if (pesage.Termine)
            {
                return RedirectToAction(nameof(BonReceptionPesage), new { id = pesage.Id });

            }
            else
            {
                return RedirectToAction(nameof(BonDechargement), new { id = pesage.Id });

            }
        }


        public async Task<IActionResult> BonDechargement(int? id)
        {
            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }

            var pesage = await _context.Pesages
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pesage == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionPesage.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //  parameters.Add("Id", "Welcome");




            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


        public async Task<IActionResult> BonReceptionPesage(int id)
        {
            if (id == null || _context.Pesages == null)
            {
                return NotFound();
            }

            var pesage = await _context.Pesages
                .Include(a => a.Parc)
                .Include(a => a.TypeDeCamion)
                .Include(a => a.TypeDeTransport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pesage == null)
            {
                return NotFound();
            }

            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportReceptionPesage.rdlc";

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
