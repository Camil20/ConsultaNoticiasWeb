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

        public async Task<IActionResult> Index(string buscar = null)
        {
            ViewData[nameof(buscar)] = buscar;

            if (string.IsNullOrEmpty(buscar))
            {
                var noticiasContext = _context.Articulos
                    .Include(a => a.Categoria)
                    .Include(a => a.Fuente)
                    .Include(a => a.Pais);
                return View(await noticiasContext.ToListAsync());
            }
            else
            {
                 var noticiasContext = _context.Articulos
                    .Include(a => a.Categoria)
                    .Include(a => a.Fuente)
                    .Include(a => a.Pais)
                    .Where(a=> a.Titulo.Contains(buscar))
                    .OrderByDescending(a=> a.Titulo);
                return View(await noticiasContext.ToListAsync());
            }
        
           
        }


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

       
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "NombreCategoria");
            ViewData["FuenteId"] = new SelectList(_context.Fuentes, "FuenteId", "NombreFuente");
            ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "NombrePais");
            return View();
        }

  
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
