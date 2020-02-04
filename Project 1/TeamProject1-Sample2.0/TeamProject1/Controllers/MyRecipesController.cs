using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamProject1.Models;

namespace TeamProject1.Controllers
{
    public class MyRecipesController : Controller
    {
        private readonly TeamProject1Context _context;

        public MyRecipesController(TeamProject1Context context)
        {
            _context = context;
        }

        // GET: MyRecipes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MyRecipe.ToListAsync());
        }

        public async Task<IActionResult> Show(string id)
        {
            var recipes = from m in _context.MyRecipe
                           join n in _context.MyRecipe_Ingredient
                           on m.Id equals n.R_id
                           join o in _context.Ingredient
                           on n.I_id equals o.Id
                           join rec_seasoning in _context.MyRecipe_Seasoning
                           on m.Id equals rec_seasoning.R_id
                           join s in _context.Seasoning
                           on rec_seasoning.S_id equals s.Id                           
                           orderby m.Name, o.Name
                           select new
                           {
                               MyRecipe = m,
                               Element = o,
                               Seasoning = s,                               
                               n.Weight,
                               w2 = rec_seasoning.Weight 
                           };

            if (!String.IsNullOrEmpty(id))
            {
                recipes = recipes.Where(m => m.MyRecipe.Name.Contains(id));
            }
            recipes = recipes.OrderBy(m => m.Element.Name).OrderBy(m => m.MyRecipe.Name);

            var tmp = await recipes.ToListAsync();
            List<RecipeDetails> lrd = new List<RecipeDetails>();
            List<int> seasonIds = new List<int>();

            foreach (var item in tmp)
            {
                Trace.Write(item.Seasoning.ToString());
                int rid = item.MyRecipe.Id;
                RecipeDetails rd = (lrd.Count > 0) ? lrd.ElementAt(lrd.Count - 1) : null;
                if (rd != null && rd.MyRecipe.Id == rid)
                {
                    Ingredient_W cew = new Ingredient_W { Ingredient = item.Element, Weight = item.Weight };
                    rd.I_common.Add(cew);
                    if (seasonIds.Contains(item.Seasoning.Id))
                    {
                        Seasoning_W sew = new Seasoning_W { Seasoning = item.Seasoning, Weight = item.w2 };
                        rd.S_common.Add(sew);
                        rd.Total_calories += item.Element.Calories * item.Weight + item.Seasoning.Calories * item.w2;
                        seasonIds.Add(item.Seasoning.Id);
                    }
                   
                }
                else
                {
                    List<Ingredient_W> my_lce = new List<Ingredient_W>();
                    Ingredient_W cew = new Ingredient_W { Ingredient = item.Element, Weight = item.Weight };
                    my_lce.Add(cew);

                    List<Seasoning_W> my_sew = new List<Seasoning_W>();
                    Seasoning_W sew = new Seasoning_W { Seasoning = item.Seasoning, Weight = item.w2 };
                    my_sew.Add(sew);
                    rd = new RecipeDetails
                    {
                        MyRecipe = item.MyRecipe,
                        I_common = my_lce,
                        S_common = my_sew
                    };
                    rd.Total_calories += item.Element.Calories * item.Weight + item.Seasoning.Calories*item.w2 ;
                    lrd.Add(rd);
                }
            }
            ViewData["TypesArray"] = new string[] { "Ingredient", "Seasoning" };
            return View(lrd);
        }
        
        // GET: MyRecipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myRecipe = await _context.MyRecipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myRecipe == null)
            {
                return NotFound();
            }

            return View(myRecipe);
        }

        // GET: MyRecipes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyRecipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MyRecipe myRecipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myRecipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myRecipe);
        }

        // GET: MyRecipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myRecipe = await _context.MyRecipe.FindAsync(id);
            if (myRecipe == null)
            {
                return NotFound();
            }
            return View(myRecipe);
        }

        // POST: MyRecipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MyRecipe myRecipe)
        {
            if (id != myRecipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myRecipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyRecipeExists(myRecipe.Id))
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
            return View(myRecipe);
        }

        // GET: MyRecipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myRecipe = await _context.MyRecipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myRecipe == null)
            {
                return NotFound();
            }

            return View(myRecipe);
        }

        // POST: MyRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myRecipe = await _context.MyRecipe.FindAsync(id);
            _context.MyRecipe.Remove(myRecipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyRecipeExists(int id)
        {
            return _context.MyRecipe.Any(e => e.Id == id);
        }
    }
}
