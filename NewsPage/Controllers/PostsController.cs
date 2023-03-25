using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsPage.Models;

namespace NewsPage.Controllers
{
    public class PostsController : Controller
    {
        private readonly NewsPageContext _db;

        public PostsController(NewsPageContext context)
        {
            _db = context;
        }
        public async Task<IActionResult> AllPosts()
        {
            return _db.Posts != null ?
                        View(await _db.Posts.ToListAsync()) :
                        Problem("Entity set 'NewsPageContext.Posts'  is null.");
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return _db.Posts != null ?
                        View(await _db.Posts.ToListAsync()) :
                        Problem("Entity set 'NewsPageContext.Posts'  is null.");
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Posts == null)
            {
                return NotFound();
            }

            var post = await _db.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewBag.ThemeId = new SelectList(_db.Themes, "ThemeId", "Name");
            ViewBag.ThemeList = new SelectList(_db.Themes, "ThemeId", "Name");

            var post = new Post();
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post, List<IFormFile> Images)
        {
            ViewBag.ThemeId = new SelectList(_db.Themes, "ThemeId", "Name");

            // Сохраняем объект Post в базе данных
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();

            // Сохраняем изображения в базу данных и на диск
            foreach (var imageFile in Images)
            {
                if (imageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(_db.WebRootPath, "images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    var image = new Image
                    {
                        PostId = post.PostId,
                        Path = "/images/" + fileName
                    };
                    _db.Images.Add(image);
                }
            }
            await _db.SaveChangesAsync();

            // Перенаправляем пользователя на страницу со списком
            return RedirectToAction(nameof(Index));
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PostId,Title,MainImage,Text")] Post post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(post);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(post);
        //}






        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ThemeId = new SelectList(_db.Themes, "ThemeId", "Name");
            ViewBag.ThemeList = new SelectList(_db.Themes, "ThemeId", "Name");
            if (id == null || _db.Posts == null)
            {
                return NotFound();
            }

            var post = await _db.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,MainImage,Text")] Post post)
        {
            ViewBag.ThemeId = new SelectList(_db.Themes, "ThemeId", "Name");
            ViewBag.ThemeList = new SelectList(_db.Themes, "ThemeId", "Name");
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(post);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Posts == null)
            {
                return NotFound();
            }

            var post = await _db.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Posts == null)
            {
                return Problem("Entity set 'NewsPageContext.Posts'  is null.");
            }
            var post = await _db.Posts.FindAsync(id);
            if (post != null)
            {
                _db.Posts.Remove(post);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return (_db.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
