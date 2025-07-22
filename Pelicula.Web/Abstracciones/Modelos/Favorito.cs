using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Favorito
    {
        public Guid IdFavorito { get; set; }
        public Guid IdUsuario { get; set; }

        public int? IdPelicula { get; set; }
        public int? IdSerie { get; set; }

        public string? Comentario { get; set; }

        public int? Puntuacion { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

    }
}
