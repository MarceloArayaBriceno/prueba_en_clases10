using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios.Peliculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IPeliculaFlujo
    {
        Task<IEnumerable<Pelicula>> Obtener(int Id);
        Task<Pelicula> ObtenerDetalle(int Id);
        Task<IEnumerable<Genero>> ObtenerGeneros();


    }
}
