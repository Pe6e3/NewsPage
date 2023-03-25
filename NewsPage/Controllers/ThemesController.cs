using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsPage.Models;

namespace NewsPage.Controllers
{
    public class ThemesController : Controller
    {
        private readonly NewsPageContext _db;

        public ThemesController(NewsPageContext context)
        {
            _db = context;
        }

        // GET: Themes
        public async Task<IActionResult> Index()
        {
              return _db.Themes != null ? 
                          View(await _db.Themes.ToListAsync()) :
                          Problem("Entity set 'NewsPageContext.Themes'  is null.");
        }

        // GET: Themes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Themes == null)
            {
                return NotFound();
            }

            var theme = await _db.Themes
                .FirstOrDefaultAsync(m => m.ThemeId == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // GET: Themes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Themes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThemeId,Name")] Theme theme)
        {
            if (ModelState.IsValid)
            {
                _db.Add(theme);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(theme);
        }

        // GET: Themes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Themes == null)
            {
                return NotFound();
            }

            var theme = await _db.Themes.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }
            return View(theme);
        }

        // POST: Themes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ThemeName")] Theme theme)
        {
            if (id != theme.ThemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(theme);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeExists(theme.ThemeId))
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
            return View(theme);
        }

        // GET: Themes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Themes == null)
            {
                return NotFound();
            }

            var theme = await _db.Themes
                .FirstOrDefaultAsync(m => m.ThemeId == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Themes == null)
            {
                return Problem("Entity set 'NewsPageContext.Themes'  is null.");
            }
            var theme = await _db.Themes.FindAsync(id);
            if (theme != null)
            {
                _db.Themes.Remove(theme);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeExists(int id)
        {
          return (_db.Themes?.Any(e => e.ThemeId == id)).GetValueOrDefault();
        }
    }
}
