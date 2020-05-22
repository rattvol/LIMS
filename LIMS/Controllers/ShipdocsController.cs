using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LIMS.Models;
using Z.EntityFramework.Plus;

namespace LIMS.Controllers
{
    public class ShipdocsController : Controller
    {
        private readonly LIMSContext _context;

        public ShipdocsController(LIMSContext context)
        {
            _context = context;
        }


        // GET: Shipdocs/Create

        public IActionResult Create(int? shipmentid = 0)
        {
            if (shipmentid == 0) return NotFound();

            Shipment shipment = _context.Shipment.Where(b => b.Id == shipmentid).Include(b => b.Supplyer).Include(b => b.Shipdocs).ThenInclude(s => s.Nomencl).First();
            ViewBag.Nomenclid = new SelectList(_context.Nomencl.Include(b => b.Prices).Where(b => !b.Deleted && b.Prices.Any(c => c.Supplyerid == shipment.Supplyerid)).ToList(), "Id", "Name");
            ViewBag.Shipment = shipment;
            return View();
        }

        // POST: Shipdocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Create(int Shipmentid, int Nomenclid, decimal Price, decimal Quantity)
        {
            if (ModelState.IsValid)
            {
                Shipdoc shipdoc = new Shipdoc() { Shipmentid = Shipmentid, Nomenclid = Nomenclid, Price = Price, Quantity = Quantity };
                try
                {
                    //Проверяем на наличие такой-же записи.
                    if (_context.Shipdoc.Any(b => b.Shipmentid == shipdoc.Shipmentid && b.Nomenclid == shipdoc.Nomenclid))
                    {
                        _context.Shipdoc.Where(b => b.Shipmentid == shipdoc.Shipmentid && b.Nomenclid == shipdoc.Nomenclid).Update(b => new Shipdoc() { Price = shipdoc.Price, Quantity = shipdoc.Quantity });
                    }
                    else
                    {
                        _context.Add(shipdoc);
                        _context.SaveChanges();
                    }

                    return ;
                } 
              catch
                {
                    return ;
                }


            }
            return ;
        }



        // GET: Shipdocs/Delete/5
        public string Delete(int? id)
        {
            if (id == null || !_context.Shipdoc.Any(b => b.Id == id))
            {
                return "NotFound";
            }

            try
            {
                _context.Shipdoc.Where(b => b.Id == id).Delete();
                return "OK";
            }
            catch
            {
                return "Error";
            }
        }


        private bool ShipdocExists(int id)
        {
            return _context.Shipdoc.Any(e => e.Id == id);
        }
        [Route("Shipdocs/GetGoods/{shipmentId}")]
        public ActionResult GetGoods(int shipmentid)
        {
            List<Shipdoc> item = _context.Shipdoc.Where(b => b.Shipmentid == shipmentid).Include(b => b.Nomencl).ToList();
            return PartialView("_GetGoods", item);
        }
        public decimal GetPrice(int shipmentid, int nomid)
        {
            int suplayerid = _context.Shipment.Find(shipmentid).Supplyerid;
            decimal price = _context.Prices.Where(b => b.Supplyerid == suplayerid && b.Nomenclid == nomid).Select(b => b.Price).FirstOrDefault();
            return price;
        }

    }
}
