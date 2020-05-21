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
    public class SupplyersController : Controller
    {
        private readonly LIMSContext _context;

        public SupplyersController(LIMSContext context)
        {
            _context = context;
        }

        // GET: Supplyers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Supplyer.Where(b=>!b.Deleted).ToListAsync());
        }

        // GET: Supplyers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyer = await _context.Supplyer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplyer == null)
            {
                return NotFound();
            }

            return View(supplyer);
        }

        // GET: Supplyers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supplyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Deleted")] Supplyer supplyer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplyer);
        }

        // GET: Supplyers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyer = await _context.Supplyer.FindAsync(id);
            if (supplyer == null)
            {
                return NotFound();
            }
            return View(supplyer);
        }

        // POST: Supplyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Deleted")] Supplyer supplyer)
        {
            if (id != supplyer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplyer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplyerExists(supplyer.Id))
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
            return View(supplyer);
        }

        // GET: Supplyers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyer = await _context.Supplyer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplyer == null)
            {
                return NotFound();
            }

            return View(supplyer);
        }

        // POST: Supplyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var supplyer = _context.Supplyer.Where(b => b.Id == id).Update(b => new Supplyer() { Deleted = true }); ;
            return RedirectToAction("Index");
        }

        private bool SupplyerExists(int id)
        {
            return _context.Supplyer.Any(e => e.Id == id);
        }
    }
}
