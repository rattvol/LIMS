using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIMS.Models;
using LIMS.Environment;
using Z.EntityFramework.Plus;

namespace LIMS.Controllers
{
    public class ShipmentsController : Controller
    {
        private readonly LIMSContext _context;

        public ShipmentsController(LIMSContext context)
        {
            _context = context;
        }

        // GET: Shipments
        public async Task<IActionResult> Index()
        {
            var lIMSContext = _context.Shipment.Where(b => !b.Deleted).Include(s => s.Supplyer).Include(b => b.Shipdocs);
            return View(await lIMSContext.ToListAsync());
        }

        // GET: Shipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipment
                .Include(s => s.Shipdocs)
                .ThenInclude(t=>t.Nomencl)
                .Include(s => s.Supplyer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // GET: Shipments/Create
        public IActionResult Create()
        {
            ViewData["Supplyerid"] = new SelectList(_context.Supplyer.Where(b => !b.Deleted), "Id", "Name");

            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Supplyerid")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                shipment.Suppdate = UnixTimeConverter.ConvertToUnixTimestamp(DateTime.Now.Date);
                shipment.Deleted = false;
                _context.Add(shipment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Shipdocs", new { shipmentid = shipment.Id });
            }
            ViewData["Supplyerid"] = new SelectList(_context.Supplyer.Where(b => !b.Deleted), "Id", "Name", shipment.Supplyerid);
            return View(shipment);
        }


        // GET: Shipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipment
                .Include(s => s.Supplyer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _context.Shipment.Where(b => b.Id == id).Update(b => new Shipment() { Deleted = true });
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipment.Any(e => e.Id == id);
        }


    }
}
