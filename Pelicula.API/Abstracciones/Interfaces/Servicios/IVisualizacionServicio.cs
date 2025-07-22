using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Servicios
{
    public interface IVisualizacionServicio
    {
        Task InsertarVisualizacion(VisualizacionPendiente visualizacion);
        Task<IEnumerable<VisualizacionPendiente>> ObtenerVisualizacionesPorUsuario(Guid idUsuario);
        Task EliminarVisualizacion(Guid idPendiente);
        Task ActualizarPrioridad(Guid idPendiente, int nuevaPrioridad);
    }
}
