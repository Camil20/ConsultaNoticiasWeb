using System;
using System.Collections.Generic;

#nullable disable

namespace AdministrarNoticias
{
    public partial class Paise
    {
        public Paise()
        {
            Articulos = new HashSet<Articulo>();
        }

        public int PaisId { get; set; }
        public string NombrePais { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}
