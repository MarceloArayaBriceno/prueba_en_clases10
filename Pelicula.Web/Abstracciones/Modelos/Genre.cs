using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Genre
    {
        [Required(ErrorMessage = "La propiedad Id es requerida")]
        public int Id { get; set; }

        [Required(ErrorMessage = "La propiedad name es requerida")]
        public string Name { get; set; }
    }

    public class GenresResponse
    {
        [Required(ErrorMessage = "La propiedad color es requerida")]
        public List<Genre> Genres { get; set; }
    }
}
