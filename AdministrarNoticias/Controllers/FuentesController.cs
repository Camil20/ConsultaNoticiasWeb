using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdministrarNoticias;

namespace AdministrarNoticias.Controllers
{
    public class FuentesController : Controller
    {
        private readonly NoticiasContext _context;

        public FuentesController(NoticiasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Fuentes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuente = await _context.Fuentes
                .FirstOrDefaultAsync(m => m.FuenteId == id);
            if (fuente == null)
            {
                return NotFound();
            }

            return View(fuente);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuenteId,NombreFuente")] Fuente fuente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fuente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fuente);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuente = await _context.Fuentes.FindAsync(id);
            if (fuente == null)
            {
                return NotFound();
            }
            return View(fuente);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuenteId,NombreFuente")] Fuente fuente)
        {
            if (id != fuente.FuenteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fuente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuenteExists(fuente.FuenteId))
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
            return View(fuente);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuente = await _context.Fuentes
                .FirstOrDefaultAsync(m => m.FuenteId == id);
            if (fuente == null)
            {
                return NotFound();
            }

            return View(fuente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fuente = await _context.Fuentes.FindAsync(id);
            _context.Fuentes.Remove(fuente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuenteExists(int id)
        {
            return _context.Fuentes.Any(e => e.FuenteId == id);
        }
    }
}
