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
    public class NomenclsController : Controller
    {
        private readonly LIMSContext _context;

        public NomenclsController(LIMSContext context)
        {
            _context = context;
        }

        // GET: Nomencls
        public async Task<IActionResult> Index()
        {
            var lIMSContext = _context.Nomencl.Include(n => n.Groupnom).Where(b=>!b.Deleted && !b.Groupnom.Deleted).OrderBy(b=>b.Groupnomid);
            return View(await lIMSContext.ToListAsync());
        }

        // GET: Nomencls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nomencl = await _context.Nomencl
                .Include(n => n.Groupnom)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nomencl == null)
            {
                return NotFound();
            }

            return View(nomencl);
        }

        // GET: Nomencls/Create
        public IActionResult Create()
        {
            ViewData["Groupnomid"] = new SelectList(_context.Groupnom, "Id", "Name");
            return View();
        }

        // POST: Nomencls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Groupnomid,Deleted")] Nomencl nomencl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nomencl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Groupnomid"] = new SelectList(_context.Groupnom, "Id", "Name", nomencl.Groupnomid);
            return View(nomencl);
        }

        // GET: Nomencls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nomencl = await _context.Nomencl.FindAsync(id);
            if (nomencl == null)
            {
                return NotFound();
            }
            ViewData["Groupnomid"] = new SelectList(_context.Groupnom, "Id", "Name", nomencl.Groupnomid);
            return View(nomencl);
        }

        // POST: Nomencls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Groupnomid,Deleted")] Nomencl nomencl)
        {
            if (id != nomencl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nomencl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NomenclExists(nomencl.Id))
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
            ViewData["Groupnomid"] = new SelectList(_context.Groupnom, "Id", "Name", nomencl.Groupnomid);
            return View(nomencl);
        }

        // GET: Nomencls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nomencl = await _context.Nomencl
                .Include(n => n.Groupnom)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nomencl == null)
            {
                return NotFound();
            }

            return View(nomencl);
        }

        // POST: Nomencls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
             _context.Nomencl.Where(b=>b.Id==id).Update(b=>new Nomencl() { Deleted=true});
            return RedirectToAction(nameof(Index));
        }

        private bool NomenclExists(int id)
        {
            return _context.Nomencl.Any(e => e.Id == id);
        }
    }
}
