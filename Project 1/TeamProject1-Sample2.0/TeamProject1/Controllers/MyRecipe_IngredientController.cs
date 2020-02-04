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
    public class MyRecipe_IngredientController : Controller
    {
        private readonly TeamProject1Context _context;

        public MyRecipe_IngredientController(TeamProject1Context context)
        {
            _context = context;
        }

        // GET: MyRecipe_Ingredient
        public async Task<IActionResult> Index()
        {
            var teamProject1Context = _context.MyRecipe_Ingredient.Include(m => m.Ingredient).Include(m => m.MyRecipe);
            return View(await teamProject1Context.ToListAsync());
        }

        // GET: MyRecipe_Ingredient/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myRecipe_Ingredient = await _context.MyRecipe_Ingredient
                .Include(m => m.Ingredient)
                .Include(m => m.MyRecipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myRecipe_Ingredient == null)
            {
                return NotFound();
            }

            return View(myRecipe_Ingredient);
        }

        // GET: MyRecipe_Ingredient/Create
        public IActionResult Create()
        {
            ViewData["I_id"] = new SelectList(_context.Ingredient, "Id", "Name");
            ViewData["R_id"] = new SelectList(_context.MyRecipe, "Id", "Name");
            return View();
        }

        // POST: MyRecipe_Ingredient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,R_id,I_id,Weight")] MyRecipe_Ingredient myRecipe_Ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myRecipe_Ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["I_id"] = new SelectList(_context.Ingredient, "Id", "Id", myRecipe_Ingredient.I_id);
            ViewData["R_id"] = new SelectList(_context.MyRecipe, "Id", "Id", myRecipe_Ingredient.R_id);
            return View(myRecipe_Ingredient);
        }

        // GET: MyRecipe_Ingredient/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myRecipe_Ingredient = await _context.MyRecipe_Ingredient.FindAsync(id);
            if (myRecipe_Ingredient == null)
            {
                return NotFound();
            }
            ViewData["I_id"] = new SelectList(_context.Ingredient, "Id", "Name", myRecipe_Ingredient.I_id);
            ViewData["R_id"] = new SelectList(_context.MyRecipe, "Id", "Name", myRecipe_Ingredient.R_id);
            return View(myRecipe_Ingredient);
        }

        // POST: MyRecipe_Ingredient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,R_id,I_id,Weight")] MyRecipe_Ingredient myRecipe_Ingredient)
        {
            if (id != myRecipe_Ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myRecipe_Ingredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyRecipe_IngredientExists(myRecipe_Ingredient.Id))
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
            ViewData["I_id"] = new SelectList(_context.Ingredient, "Id", "Id", myRecipe_Ingredient.I_id);
            ViewData["R_id"] = new SelectList(_context.MyRecipe, "Id", "Id", myRecipe_Ingredient.R_id);
            return View(myRecipe_Ingredient);
        }

        // GET: MyRecipe_Ingredient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myRecipe_Ingredient = await _context.MyRecipe_Ingredient
                .Include(m => m.Ingredient)
                .Include(m => m.MyRecipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myRecipe_Ingredient == null)
            {
                return NotFound();
            }

            return View(myRecipe_Ingredient);
        }

        // POST: MyRecipe_Ingredient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myRecipe_Ingredient = await _context.MyRecipe_Ingredient.FindAsync(id);
            _context.MyRecipe_Ingredient.Remove(myRecipe_Ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyRecipe_IngredientExists(int id)
        {
            return _context.MyRecipe_Ingredient.Any(e => e.Id == id);
        }
    }
}
