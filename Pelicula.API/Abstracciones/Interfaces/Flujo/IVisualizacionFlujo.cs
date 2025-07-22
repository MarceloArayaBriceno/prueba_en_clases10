using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IVisualizacionFlujo
    {
        Task InsertarVisualizacion(VisualizacionPendiente visualizacion);
        Task<IEnumerable<object>> ObtenerVisualizacionesPorUsuario(Guid idUsuario);
        Task EliminarVisualizacion(Guid idPendiente);
        Task ActualizarPrioridad(Guid idPendiente, int nuevaPrioridad);
    }
}
