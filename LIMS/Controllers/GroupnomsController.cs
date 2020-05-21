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
    public class GroupnomsController : Controller
    {
        private readonly LIMSContext _context;

        public GroupnomsController(LIMSContext context)
        {
            _context = context;
        }

        // GET: Groupnoms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Groupnom.Where(b=>!b.Deleted).ToListAsync());
        }

        // GET: Groupnoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupnom = await _context.Groupnom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupnom == null)
            {
                return NotFound();
            }

            return View(groupnom);
        }

        // GET: Groupnoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groupnoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Deleted")] Groupnom groupnom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groupnom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groupnom);
        }

        // GET: Groupnoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupnom = await _context.Groupnom.FindAsync(id);
            if (groupnom == null)
            {
                return NotFound();
            }
            return View(groupnom);
        }

        // POST: Groupnoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Deleted")] Groupnom groupnom)
        {
            if (id != groupnom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groupnom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupnomExists(groupnom.Id))
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
            return View(groupnom);
        }

        // GET: Groupnoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupnom = await _context.Groupnom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupnom == null)
            {
                return NotFound();
            }

            return View(groupnom);
        }

        // POST: Groupnoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.Groupnom.Where(b => b.Id == id).Update(b => new Groupnom() { Deleted = true });
            return RedirectToAction(nameof(Index));
        }

        private bool GroupnomExists(int id)
        {
            return _context.Groupnom.Any(e => e.Id == id);
        }
    }
}
