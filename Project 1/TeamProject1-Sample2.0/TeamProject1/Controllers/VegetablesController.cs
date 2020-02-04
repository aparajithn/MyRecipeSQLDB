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
    public class VegetablesController : Controller
    {
        private readonly TeamProject1Context _context;

        public VegetablesController(TeamProject1Context context)
        {
            _context = context;
        }

        // GET: Vegetables
        public async Task<IActionResult> Index()
        {
            var teamProject1Context = _context.Vegetable.Include(v => v.Ingredient);
            return View(await teamProject1Context.ToListAsync());
        }

        // GET: Vegetables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vegetable = await _context.Vegetable
                .Include(m => m.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vegetable == null)
            {
                return NotFound();
            }

            return View(vegetable);
        }

        // GET: Vegetables/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Ingredient, "Id", "Id");
            return View();
        }

        // POST: Vegetables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Calories")] Ingredient ingredient, [Bind("Fiber")] Vegetable vegetable)
        {
            if (ModelState.IsValid)
            {
                _context.Ingredient.Add(ingredient);
                vegetable.Id = ingredient.Id;
                _context.Add(vegetable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Ingredient, "Id", "Id", vegetable.Id);
            return View(vegetable);
        }

        // GET: Vegetables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vegetable = await _context.Vegetable.FindAsync(id);
            var ingredient = await _context.Ingredient.FindAsync(id);
            if (vegetable == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Ingredient, "Id", "Id", vegetable.Id);
            return View(vegetable);
        }

        // POST: Vegetables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Fiber")] Ingredient ingredient, [Bind("Id,Fiber")] Vegetable vegetable)
        {
            if (id != vegetable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ingredient.Id = id;
                    _context.Ingredient.Update(ingredient);
                    _context.Update(vegetable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VegetableExists(vegetable.Id))
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
            ViewData["Id"] = new SelectList(_context.Ingredient, "Id", "Id", vegetable.Id);
            return View(vegetable);
        }

        // GET: Vegetables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vegetable = await _context.Vegetable
                .Include(m => m.Ingredient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vegetable == null)
            {
                return NotFound();
            }

            return View(vegetable);
        }

        // POST: Vegetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context.Ingredient.FindAsync(id);
            _context.Ingredient.Remove(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VegetableExists(int id)
        {
            return _context.Vegetable.Any(e => e.Id == id);
        }
    }
}
