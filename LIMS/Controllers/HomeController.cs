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
using Z.EntityFramework.Plus;

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
            int startDateUnix = UnixTimeConverter.ConvertToUnixTimestamp(startDate ?? DateTime.Parse("01.01.1970"));
            int finishDateUnix = UnixTimeConverter.ConvertToUnixTimestamp(finishDate ?? DateTime.Now);
            ViewBag.StartDate = (startDate ?? DateTime.Parse("01.01.1970")).ToShortDateString();
            ViewBag.FinishDate = (finishDate ?? DateTime.Now).ToShortDateString();
            List<Groupnom> groupnoms = await _context.Groupnom
                    .Where(b => !b.Deleted)
                    .OrderBy(b => b.Name)
                    .IncludeFilter(b => b.Nomencl
                                         .Where(c => !c.Deleted&&c.Shipdoc
                                                        .Any(d=>d.Nomenclid==c.Id&&d.Shipment.Suppdate>=startDateUnix&&d.Shipment.Suppdate <= finishDateUnix)))
                    .ToListAsync();

            ViewBag.Supplyer = await _context.Supplyer
                .Where(b => !b.Deleted)
                .IncludeFilter(b => b.Shipment.Where(c => !c.Deleted && c.Suppdate >= startDateUnix && c.Suppdate <= finishDateUnix))
                .Include(b => b.Shipment)
                .ThenInclude(c => c.Shipdocs)
                .ToListAsync();

            ViewBag.Shipdocs = await _context.Shipdoc
                .Include(b => b.Shipment)
                .Where(b => !b.Shipment.Deleted && startDateUnix <= b.Shipment.Suppdate && finishDateUnix >= b.Shipment.Suppdate)
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
