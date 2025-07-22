using Abstracciones.Modelos.Servicios.Peliculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Servicios
{
    public interface ISerieServicio
    {
        Task<IEnumerable<Genero>>ObtenerGenerosSeries();

        Task<IEnumerable<Serie>>ObtenerSeriesPorGenero(int Id);

        Task<Serie> ObtenerSeriesDetalle(int Id);
    }
}
