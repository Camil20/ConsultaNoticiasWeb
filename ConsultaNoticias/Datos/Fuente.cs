using System;
using System.Collections.Generic;

#nullable disable

namespace ConsultaNoticias
{
    public partial class Fuente
    {
        public Fuente()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int FuenteId { get; set; }
        public string NombreFuente { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}
