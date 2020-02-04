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
    public class SeasoningsController : Controller
    {
        private readonly TeamProject1Context _context;

        public SeasoningsController(TeamProject1Context context)
        {
            _context = context;
        }

        // GET: Seasonings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seasoning.ToListAsync());
        }

    }
}