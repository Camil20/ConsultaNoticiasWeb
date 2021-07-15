using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConsultaNoticias;

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
        
        //public IList<Categoria> displaydata { get; set; }

    
        public async Task OnGetAsync(string buscar = null)
        {
            
            ViewData[nameof(buscar)] = buscar;
            if(string.IsNullOrEmpty(buscar))
            {
                //displaydata = await _context.Categorias.ToListAsync();
                Articulo = await _context.Articulos
               .Include(a => a.Categoria)
               .Include(a => a.Fuente)
               .Include(a => a.Pais).ToListAsync();
            }
            else
            {
                //displaydata = await _context.Categorias.ToListAsync();
                Articulo = await _context.Articulos
                .Include(a => a.Categoria)
                .Include(a => a.Fuente)
                .Include(a => a.Pais)
                .Where(x => x.Titulo.Contains(buscar)).
                ToListAsync();
            }

            
        }
    }
}
