using InterfacePontBascule.Business;
using InterfacePontBascule.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterfacePontBascule.Controllers
{
    public class BusinessController : Controller
    {
        // GET: BusinessController
  

         private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
       // private readonly ILogger _logger;
        private  IComPortUsage _comPortUsage;
        private INumTicketBonManagement _numTicketBonManagement;
        public BusinessController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IComPortUsage comPortUsage, INumTicketBonManagement numTicketBonManagement)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
          //  _logger = logger;
            _comPortUsage = comPortUsage;
            _numTicketBonManagement = numTicketBonManagement;
        }


        public ActionResult Peser()
        {
            return Content(_comPortUsage.ReadData());
        }
    }
}
