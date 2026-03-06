using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerfumeStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace PerfumeStore.Controllers
{
    public class PerfumesController : Controller
    {
        private readonly PerfumeStoreContext _context;

        public PerfumesController(PerfumeStoreContext context)
        {
            _context = context;
        }

        // GET: Perfumes (ВІДКРИТО ДЛЯ ВСІХ)
        public async Task<IActionResult> Index()
        {
            var perfumeStoreContext = _context.Perfumes.Include(p => p.Brand).Include(p => p.Category);
            return View(await perfumeStoreContext.ToListAsync());
        }

        // GET: Perfumes/Details/5 (ВІДКРИТО ДЛЯ ВСІХ)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PerfumeId == id);
            if (perfume == null)
            {
                return NotFound();
            }

            return View(perfume);
        }

        // GET: Perfumes/Create (ТІЛЬКИ АДМІН)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Perfumes/Create (ТІЛЬКИ АДМІН)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("PerfumeId,Name,BrandId,CategoryId,Price,Volume,StockQuantity")] Perfume perfume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", perfume.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", perfume.CategoryId);
            return View(perfume);
        }

        // GET: Perfumes/Edit/5 (ТІЛЬКИ АДМІН)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes.FindAsync(id);
            if (perfume == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", perfume.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", perfume.CategoryId);
            return View(perfume);
        }

        // POST: Perfumes/Edit/5 (ТІЛЬКИ АДМІН)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("PerfumeId,Name,BrandId,CategoryId,Price,Volume,StockQuantity")] Perfume perfume)
        {
            if (id != perfume.PerfumeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfumeExists(perfume.PerfumeId))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", perfume.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", perfume.CategoryId);
            return View(perfume);
        }

        // GET: Perfumes/Delete/5 (ТІЛЬКИ АДМІН)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PerfumeId == id);
            if (perfume == null)
            {
                return NotFound();
            }

            return View(perfume);
        }

        // POST: Perfumes/Delete/5 (ТІЛЬКИ АДМІН)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perfume = await _context.Perfumes.FindAsync(id);
            if (perfume != null)
            {
                _context.Perfumes.Remove(perfume);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // =========================================================================
        // НОВИЙ МЕТОД: ПОКУПКА ПАРФУМУ (Для всіх зареєстрованих користувачів)
        // =========================================================================
        [HttpPost]
        [Authorize] // Купувати можуть тільки ті, хто увійшов в акаунт
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(int perfumeId, int quantity)
        {
            // Беремо email користувача, який зараз на сайті
            var userName = User.Identity.Name;

            try
            {
                // Викликаємо нашу збережену процедуру з бази даних
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_AddOrder @PerfumeId = {0}, @CustomerName = {1}, @Quantity = {2}",
                    perfumeId, userName, quantity);

                TempData["SuccessMessage"] = $"Замовлення успішно оформлено! Ви придбали {quantity} шт.";
            }
            catch (Exception)
            {
                TempData["SuccessMessage"] = "Помилка при оформленні замовлення. Можливо, на складі недостатньо товару.";
            }

            // Повертаємо користувача назад на сторінку товару
            return RedirectToAction(nameof(Details), new { id = perfumeId });
        }

        private bool PerfumeExists(int id)
        {
            return _context.Perfumes.Any(e => e.PerfumeId == id);
        }
    }
}