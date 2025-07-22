using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    using System.Collections.Generic;

    public class Serie
    {

        public List<int> IdsGeneros { get; set; }
        public int Id { get; set; }

        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string FechaLanzamiento { get; set; }
        public string Titulo { get; set; }
        public double Calificacion { get; set; }

    }

}
