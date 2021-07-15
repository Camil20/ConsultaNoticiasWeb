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
    public class ArticulosController : Controller
    {
        private readonly NoticiasContext _context;

        public ArticulosController(NoticiasContext context)
        {
            _context = context;
        }

        // GET: Articulos
        public async Task<IActionResult> Index()
        {
            var noticiasContext = _context.Articulos.Include(a => a.Categoria).Include(a => a.Fuente).Include(a => a.Pais);
            return View(await noticiasContext.ToListAsync());
        }

        // GET: Articulos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.Categoria)
                .Include(a => a.Fuente)
                .Include(a => a.Pais)
                .FirstOrDefaultAsync(m => m.ArticuloId == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // GET: Articulos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria");
            ViewData["FuenteId"] = new SelectList(_context.Fuentes, "FuenteId", "NombreFuente");
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "NombrePais");
            return View();
        }

        // POST: Articulos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticuloId,Titulo,Descripcion,Autor,Url,UrlToImage,FechaPublicacion,Contenido,CategoriaId,PaisId,FuenteId")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria", articulo.CategoriaId);
            ViewData["FuenteId"] = new SelectList(_context.Fuentes, "FuenteId", "NombreFuente", articulo.FuenteId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "NombrePais", articulo.PaisId);
            return View(articulo);
        }

        // GET: Articulos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria", articulo.CategoriaId);
            ViewData["FuenteId"] = new SelectList(_context.Fuentes, "FuenteId", "NombreFuente", articulo.FuenteId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "NombrePais", articulo.PaisId);
            return View(articulo);
        }

        // POST: Articulos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticuloId,Titulo,Descripcion,Autor,Url,UrlToImage,FechaPublicacion,Contenido,CategoriaId,PaisId,FuenteId")] Articulo articulo)
        {
            if (id != articulo.ArticuloId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloExists(articulo.ArticuloId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria", articulo.CategoriaId);
            ViewData["FuenteId"] = new SelectList(_context.Fuentes, "FuenteId", "NombreFuente", articulo.FuenteId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "NombrePais", articulo.PaisId);
            return View(articulo);
        }

        // GET: Articulos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.Categoria)
                .Include(a => a.Fuente)
                .Include(a => a.Pais)
                .FirstOrDefaultAsync(m => m.ArticuloId == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            _context.Articulos.Remove(articulo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.ArticuloId == id);
        }
    }
}
