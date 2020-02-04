using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamProject1.Models;

namespace TeamProject1.Controllers
{
    public class SpicesController : Controller
    {
        private readonly TeamProject1Context _context;

        public SpicesController(TeamProject1Context context)
        {
            _context = context;
        }

        // GET: Spices
        public async Task<IActionResult> Index()
        {
            var teamProject1Context = _context.Spice.Include(m => m.Seasoning);
            return View(await teamProject1Context.ToListAsync());
        }

        // GET: Spices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spice = await _context.Spice
                .Include(m => m.Seasoning)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spice == null)
            {
                return NotFound();
            }

            return View(spice);
        }

        // GET: Spices/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Seasoning, "Id", "Id");
            return View();
        }

        // POST: Spices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Calories,Place_of_Origin")] Seasoning seasoning, [Bind("Hotness")] Spice spice)
        {
            if (ModelState.IsValid)
            {
                _context.Seasoning.Add(seasoning);
                spice.Id = seasoning.Id;
                _context.Add(spice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Seasoning, "Id", "Id", spice.Id);
            return View(spice);
        }

        // GET: Spices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spice = await _context.Spice.FindAsync(id);
            var seasoning = await _context.Seasoning.FindAsync(id);
            if (spice == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Seasoning, "Id", "Id", spice.Id);
            return View(spice);
        }

        // POST: Spices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Calories,Place_of_Origin")] Seasoning seasoning, [Bind("Hotness")] Spice spice)
        {
            if (id != spice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    seasoning.Id = id;
                    _context.Seasoning.Update(seasoning);
                    _context.Update(spice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpiceExists(spice.Id))
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
            ViewData["Id"] = new SelectList(_context.Seasoning, "Id", "Id", spice.Id);
            return View(spice);
        }

        // GET: Spices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spice = await _context.Spice
                .Include(m => m.Seasoning)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spice == null)
            {
                return NotFound();
            }

            return View(spice);
        }

        // POST: Spices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seasoning = await _context.Seasoning.FindAsync(id);
            _context.Seasoning.Remove(seasoning);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpiceExists(int id)
        {
            return _context.Spice.Any(e => e.Id == id);
        }
    }
}
