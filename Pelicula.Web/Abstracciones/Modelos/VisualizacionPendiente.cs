using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class VisualizacionPendiente
    {
        public Guid IdPendiente { get; set; }
        public Guid IdUsuario { get; set; }
        public int? IdPelicula { get; set; }
        public int? IdSerie { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public int Prioridad { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}