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
    public class HerbsController : Controller
    {
        private readonly TeamProject1Context _context;

        public HerbsController(TeamProject1Context context)
        {
            _context = context;
        }

        // GET: Herbs
        public async Task<IActionResult> Index()
        {
            var teamProject1Context = _context.Herb.Include(h => h.Seasoning);
            return View(await teamProject1Context.ToListAsync());
        }

        // GET: Herbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var herb = await _context.Herb
                .Include(m => m.Seasoning)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (herb == null)
            {
                return NotFound();
            }

            return View(herb);
        }

        // GET: Herbs/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Seasoning, "Id", "Id");
            return View();
        }

        // POST: Herbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Calories,Place_of_Origin")] Seasoning seasoning, Herb herb )  
        {
            if (ModelState.IsValid)
            {
                _context.Seasoning.Add(seasoning);
                herb.Id = seasoning.Id;
                _context.Add(herb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Seasoning, "Id", "Id", herb.Id);
            return View(herb);
        }

        // GET: Herbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var herb = await _context.Herb.FindAsync(id);
            var seasoning = await _context.Seasoning.FindAsync(id);
            if (herb == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Seasoning, "Id", "Id", herb.Id);
            return View(herb);
        }

        // POST: Herbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Calories,Place_of_Origin")] Seasoning seasoning, Herb herb)
        {
            if (id != herb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    seasoning.Id = id;
                    _context.Seasoning.Update(seasoning);
                    _context.Update(herb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HerbExists(herb.Id))
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
            ViewData["Id"] = new SelectList(_context.Ingredient, "Id", "Id", herb.Id);
            return View(herb);
        }

        // GET: Herbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var herb = await _context.Herb
                .Include(m => m.Seasoning)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (herb == null)
            {
                return NotFound();
            }

            return View(herb);
        }

        // POST: Herbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seasoning = await _context.Seasoning.FindAsync(id);
            _context.Seasoning.Remove(seasoning);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HerbExists(int id)
        {
            return _context.Herb.Any(e => e.Id == id);
        }
    }
}
