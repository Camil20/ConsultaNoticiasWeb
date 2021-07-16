using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AdministrarNoticias
{
    public partial class Articulo
    {
       
        public int ArticuloId { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Autor { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Url { get; set; }
        public string UrlToImage { get; set; }

        public DateTime? FechaPublicacion { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Contenido { get; set; }
        public int? CategoriaId { get; set; }
        public int? PaisId { get; set; }
        public int? FuenteId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Fuente Fuente { get; set; }
        public virtual Paise Pais { get; set; }
    }
}
