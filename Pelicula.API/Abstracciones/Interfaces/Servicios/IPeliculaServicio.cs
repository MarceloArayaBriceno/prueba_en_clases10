using Abstracciones.Modelos.Servicios.Peliculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Servicios
{
    public interface IPeliculaServicio
    {

        Task<IEnumerable<Genero>> ObtenerGenerosPeliculas();

        Task<IEnumerable<Pelicula>>ObtenerPeliculasPorGenero(int Id);

        Task<Pelicula> ObtenerPeliculasDetalle(int Id);

    }
}
