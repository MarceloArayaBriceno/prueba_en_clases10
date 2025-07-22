using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Abstracciones.Modelos
{

        public class Pelicula
        {
            public List<int> IdsGeneros { get; set; }

            public int Id { get; set; }

            public string Descripcion { get; set; }

            public string Imagen { get; set; }

            public string ImagenFondo { get; set; }

            public string FechaLanzamiento { get; set; }

            public string Titulo { get; set; }

            public double Calificacion { get; set; }
        }
}

