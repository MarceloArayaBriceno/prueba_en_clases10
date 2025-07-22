using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Genero
    {
        [Required(ErrorMessage = "La propiedad Id es requerida")]
        public int Id { get; set; }

        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        public string Nombre { get; set; }
    }
    public class GenerosResponse
    {
        [Required(ErrorMessage = "La propiedad color es requerida")]
        public List<Genero> Generos { get; set; }

    }
}
