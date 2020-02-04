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
    public class MeatsController : Controller
    {
        private readonly TeamProject1Context _context;

        public MeatsController(TeamProject1Context context)
        {
            _context = context;
        }

        // GET: Meats
        public async Task<IActionResult> Index()
        {
            var teamProject1Context = _context.Meat.Include(m => m.Ingredient);
            return View(await teamProject1Context.ToListAsync());
        }

        // GET: Meats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meat = await _context.Meat
                .Include(m => m.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meat == null)
            {
                return NotFound();
            }

            return View(meat);
        }

        // GET: Meats/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Ingredient, "Id", "Id");
            return View();
        }

        // POST: Meats/Create 
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Calories")] Ingredient ingredient, [Bind("Protein")] Meat meat)
        {
            if (ModelState.IsValid)
            {
                _context.Ingredient.Add(ingredient);
                meat.Id = ingredient.Id;
                _context.Add(meat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Ingredient, "Id", "Id", meat.Id);
            return View(meat);
        }

        // GET: Meats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meat = await _context.Meat.FindAsync(id);
            var ingredient = await _context.Ingredient.FindAsync(id);
            if (meat == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Ingredient, "Id", "Id", meat.Id);
            return View(meat);
        }

        // POST: Meats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Calories")] Ingredient ingredient, [Bind("Id,Protein")] Meat meat)
        {
            if (id != meat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ingredient.Id = id;
                    _context.Ingredient.Update(ingredient);
                    _context.Update(meat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeatExists(meat.Id))
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
            ViewData["Id"] = new SelectList(_context.Ingredient, "Id", "Id", meat.Id);
            return View(meat);
        }

        // GET: Meats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meat = await _context.Meat
                .Include(m => m.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meat == null)
            {
                return NotFound();
            }

            return View(meat);
        }

        // POST: Meats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context.Ingredient.FindAsync(id);
            _context.Ingredient.Remove(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeatExists(int id)
        {
            return _context.Meat.Any(e => e.Id == id);
        }
    }
}
