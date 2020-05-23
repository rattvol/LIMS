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
    public class PricesController : Controller
    {
        private readonly LIMSContext _context;

        public PricesController(LIMSContext context)
        {
            _context = context;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            var lIMSContext = _context.Prices.Include(p => p.Nomencl).ThenInclude(nom => nom.Groupnom).Include(p => p.Supplyer).OrderBy(b=>b.Supplyerid).ThenBy(b=>b.Nomencl.Groupnomid);
            return View(await lIMSContext.ToListAsync());
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prices = await _context.Prices
                .Include(p => p.Nomencl)
                .ThenInclude(nom => nom.Groupnom)
                .Include(p => p.Supplyer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prices == null)
            {
                return NotFound();
            }

            return View(prices);
        }

        // GET: Prices/Create
        public IActionResult Create()
        {
            ViewData["Nomenclid"] = new SelectList(_context.Nomencl.Where(b => !b.Deleted), "Id", "Name");
            ViewData["Supplyerid"] = new SelectList(_context.Supplyer.Where(b=>!b.Deleted), "Id", "Name");
            ViewData["Groupid"] = new SelectList(_context.Groupnom.Where(b => !b.Deleted), "Id", "Name");
            return View();
        }

        // POST: Prices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Supplyerid,Nomenclid,Price")] Prices prices)
        {
            if (ModelState.IsValid)
            {
                prices.Price = Math.Round(prices.Price, 2);
                try
                {
                    //Проверяем на наличие такой-же записи.
                    if (_context.Prices.Any(b => b.Supplyerid == prices.Supplyerid && b.Nomenclid == prices.Nomenclid))
                    {
                        _context.Prices.Where(b => b.Supplyerid == prices.Supplyerid && b.Nomenclid == prices.Nomenclid).Update(b=>new Prices() { Price=prices.Price});
                    }
                    else
                    {
                        _context.Add(prices);
                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PricesExists(prices.Id))
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
            ViewData["Nomenclid"] = new SelectList(_context.Nomencl.Where(b => !b.Deleted), "Id", "Name", prices.Nomenclid);
            ViewData["Supplyerid"] = new SelectList(_context.Supplyer.Where(b => !b.Deleted), "Id", "Name", prices.Supplyerid);
            ViewData["Groupid"] = new SelectList(_context.Groupnom.Where(b => !b.Deleted), "Id", "Name", GetGroupId(prices.Nomenclid));
            return View(prices);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prices = await _context.Prices.FindAsync(id);
            if (prices == null)
            {
                return NotFound();
            }
            ViewData["Nomenclid"] = new SelectList(_context.Nomencl.Where(b => !b.Deleted), "Id", "Name", prices.Nomenclid);
            ViewData["Supplyerid"] = new SelectList(_context.Supplyer.Where(b => !b.Deleted), "Id", "Name", prices.Supplyerid);
            ViewData["Groupid"] = new SelectList(_context.Groupnom.Where(b => !b.Deleted), "Id", "Name", GetGroupId(prices.Nomenclid));
            return View(prices);
        }

        // POST: Prices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Supplyerid,Nomenclid,Price")] Prices prices)
        {
            if (id != prices.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid&& !_context.Prices.Any(b =>b.Id!=prices.Id & b.Supplyerid == prices.Supplyerid & b.Nomenclid == prices.Nomenclid))
            {
                prices.Price = Math.Round(prices.Price, 2);
                try
                {
                    _context.Update(prices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PricesExists(prices.Id))
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
            ViewData["Nomenclid"] = new SelectList(_context.Nomencl.Where(b => !b.Deleted), "Id", "Name", prices.Nomenclid);
            ViewData["Supplyerid"] = new SelectList(_context.Supplyer.Where(b => !b.Deleted), "Id", "Name", prices.Supplyerid);
            ViewData["Groupid"] = new SelectList(_context.Groupnom.Where(b => !b.Deleted), "Id", "Name", GetGroupId(prices.Nomenclid));
            return View(prices);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prices = await _context.Prices
                .Include(p => p.Nomencl)
                .ThenInclude(nom=>nom.Groupnom)
                .Include(p => p.Supplyer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prices == null)
            {
                return NotFound();
            }

            return View(prices);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prices = await _context.Prices.FindAsync(id);
            _context.Prices.Remove(prices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PricesExists(int id)
        {
            return _context.Prices.Any(e => e.Id == id);
        }

        [Route("GetNomencl/{groupid}")]
        public ActionResult GetNomencl(int groupid)
        {
            List<Nomencl> item = _context.Nomencl.Where(b =>!b.Deleted && b.Groupnomid == groupid).ToList();
            return PartialView("_GetNomencl", item);
        }
        int GetGroupId (int nomid)
        {
            return _context.Nomencl.Where(b => b.Id == nomid).Select(b => b.Groupnomid).FirstOrDefault();
        }
    }
}
