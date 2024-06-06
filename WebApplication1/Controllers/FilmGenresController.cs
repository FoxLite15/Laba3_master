using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FilmGenresController : Controller
    {
        private readonly DataContext _context;

        public FilmGenresController(DataContext context)
        {
            _context = context;
        }

        // GET: FilmGenres
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.FilmGenres.Include(f => f.Film).Include(f => f.Genre);
            return View(await dataContext.ToListAsync());
        }

        // GET: FilmGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmGenre = await _context.FilmGenres
                .Include(f => f.Film)
                .Include(f => f.Genre)
                .FirstOrDefaultAsync(m => m.FilmGenreId == id);
            if (filmGenre == null)
            {
                return NotFound();
            }

            return View(filmGenre);
        }

        // GET: FilmGenres/Create
        public IActionResult Create()
        {
            ViewData["FilmId"] = new SelectList(_context.Filmss, "FilmId", "Title");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name");
            return View();
        }

        // POST: FilmGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmId,GenreId")] FilmGenre filmGenre)
        {
				if (ModelState.IsValid)
				{
					var filmGenreTemp = await _context.FilmGenres
						.Include(b => b.Film)
						.Include(b => b.Genre).ToListAsync();
					if (filmGenreTemp.Count == 0)
						++filmGenre.FilmGenreId;
					else
					{

						filmGenre.FilmGenreId = filmGenreTemp[^1].FilmGenreId + 1;
					}
					_context.Add(filmGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmId"] = new SelectList(_context.Filmss, "FilmId", "FilmId", filmGenre.FilmId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", filmGenre.GenreId);
            return View(filmGenre);
        }

        // GET: FilmGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmGenre = await _context.FilmGenres
                          .Include(f => f.Film)
                          .Include(f => f.Genre)
                          .FirstOrDefaultAsync(m => m.FilmGenreId == id);
            if (filmGenre == null)
            {
                return NotFound();
            }
            ViewData["FilmId"] = new SelectList(_context.Filmss, "FilmId", "FilmId", filmGenre.FilmId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", filmGenre.GenreId);
            return View(filmGenre);
        }

        // POST: FilmGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmId,GenreId")] FilmGenre filmGenre)
        {
            if (id != filmGenre.FilmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmGenreExists(filmGenre.FilmGenreId))
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
            ViewData["FilmId"] = new SelectList(_context.Filmss, "FilmId", "FilmId", filmGenre.FilmId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", filmGenre.GenreId);
            return View(filmGenre);
        }

        // GET: FilmGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmGenre = await _context.FilmGenres
                .Include(f => f.Film)
                .Include(f => f.Genre)
                .FirstOrDefaultAsync(m => m.FilmGenreId == id);
            if (filmGenre == null)
            {
                return NotFound();
            }

            return View(filmGenre);
        }

        // POST: FilmGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filmGenre = await _context.FilmGenres
                          .Include(f => f.Film)
                          .Include(f => f.Genre)
                          .FirstOrDefaultAsync(m => m.FilmGenreId == id);
            if (filmGenre != null)
            {
                _context.FilmGenres.Remove(filmGenre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmGenreExists(int id)
        {
            return _context.FilmGenres.Any(e => e.FilmGenreId == id);
        }
    }
}
