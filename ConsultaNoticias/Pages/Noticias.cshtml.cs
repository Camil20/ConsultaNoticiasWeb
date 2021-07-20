using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConsultaNoticias;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConsultaNoticias.Pages
{
    public class NoticiasModel : PageModel
    {
        private readonly ConsultaNoticias.NoticiasContext _context;

        public NoticiasModel(ConsultaNoticias.NoticiasContext context)
        {
            _context = context;
        }

        public IList<Articulo> Articulo { get;set; }

        public IList<Categoria> Displaydata { get; set; }

        public async Task OnGetAsync(string buscar = null, string busqueda = null)
        {
            ViewData[nameof(busqueda)] = busqueda;
            ViewData[nameof(buscar)] = buscar;
            

            if (string.IsNullOrEmpty(buscar))
            {
                
                Articulo = await _context.Articulos
               .Include(a => a.Categoria)
               .Include(a => a.Fuente)
               .Include(a => a.Pais).ToListAsync();
            }
            else
            {
                
                Articulo = await _context.Articulos
                .Include(a => a.Categoria)
                .Include(a => a.Fuente)
                .Include(a => a.Pais)
                .Where(x => x.Titulo.Contains(buscar))
                .ToListAsync();
            }

            if (string.IsNullOrEmpty(busqueda))
            {
                Displaydata = await _context.Categorias.ToListAsync();
            }
            else
            {
                Displaydata = await _context.Categorias.
                    Where(x => x.NombreCategoria.Contains(busqueda))
                    .ToListAsync();
            };

        }


        //public IEnumerable<SelectListItem> GetCategorias()
        //{
        //    using (var context = new NoticiasContext())
        //    {
        //        List<SelectListItem> Articulos = context.Categorias.AsNoTracking()
        //            .OrderBy(n => n)
        //                .Select(n =>
        //                new SelectListItem
        //                {
        //                    Value = n.CategoriaId.ToString(),
        //                    Text = n.NombreCategoria
        //                }).ToList();
        //        var NombreCateg = new SelectListItem()
        //        {
        //            Value = null,
        //            Text = "--- select country ---"
        //        };
        //        Articulos.Insert(0, NombreCateg);
        //        return new SelectList(Articulos, "CategoriaId", "NombreCategoria");
        //    }
        //}
    }
}
