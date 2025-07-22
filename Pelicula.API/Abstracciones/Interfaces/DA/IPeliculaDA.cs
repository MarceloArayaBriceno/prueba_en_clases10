
using Abstracciones.Modelos.Servicios.Peliculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IPeliculaDA
    {
        public Task<int> Agregar(Pelicula pelicula, int rol);

    }
}