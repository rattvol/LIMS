using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LIMS.Models;
using Microsoft.EntityFrameworkCore;
using LIMS.Environment;

namespace LIMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly LIMSContext _context;
        private readonly ILogger<HomeController> _logger;

       public HomeController(LIMSContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(DateTime? startDate, DateTime? finishDate)
        {
                int startDateUnix = UnixTimeConverter.ConvertToUnixTimestamp(startDate??DateTime.Parse("01.01.1970"));
                int finishDateUnix = UnixTimeConverter.ConvertToUnixTimestamp(finishDate ?? DateTime.Now);
            ViewBag.StartDate = (startDate ?? DateTime.Parse("01.01.1970")).ToShortDateString();
            ViewBag.FinishDate = (finishDate ?? DateTime.Now).ToShortDateString();
            List<Groupnom>  groupnoms = await _context.Groupnom
                    .Where(b => !b.Deleted)
                    .OrderBy(b => b.Name)
                    .Include(b => b.Nomencl)
                    .ToListAsync();

                ViewBag.Supplyer = await _context.Supplyer
                    .Where(b => !b.Deleted)
                    .ToListAsync();

                ViewBag.Shipdocs = await _context.Shipdoc
                    .Include(b => b.Shipment)
                    .Where(b=>!b.Shipment.Deleted && startDateUnix<=b.Shipment.Suppdate && finishDateUnix>=b.Shipment.Suppdate)
                    .OrderBy(b => b.Shipment.Suppdate)
                    .ThenBy(c => c.Nomenclid)
                    .ToListAsync();
            return View(groupnoms);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
