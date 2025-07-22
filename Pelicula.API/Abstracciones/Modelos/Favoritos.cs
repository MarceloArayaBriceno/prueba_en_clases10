using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{

    public class FavoritoBase
    {
        [Required(ErrorMessage = "El campo IdUsuario es obligatorio")]
        public Guid IdUsuario { get; set; }

        public string? Email { get; set; }
        public int? IdPelicula { get; set; }
        public int? IdSerie { get; set; }

        public string? Comentario { get; set; }

        [Range(1, 10, ErrorMessage = "La puntuación debe estar entre 1 y 10")]
        public int? Puntuacion { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }

    public class FavoritoRequest : FavoritoBase
    {
    }

    public class FavoritoResponse : FavoritoBase
    {
        public Guid IdFavorito { get; set; }

        public string? NombreUsuario { get; set; }
        public string? TituloPelicula { get; set; }
        public string? TituloSerie { get; set; }
    }

    public class FavoritoDetalle : FavoritoResponse
    {
        public bool RevisionValida { get; set; }
        public bool RegistroValido { get; set; }
    }

}
