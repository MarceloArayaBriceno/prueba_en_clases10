using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios.Peliculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ISerieFlujo
    {
        Task<IEnumerable<Serie>> Obtener(int Id);
        Task<Serie> ObtenerDetalle(int Id);
        Task<IEnumerable<Genero>> ObtenerGeneros();
    }
}
