using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IVisualizacionDA
    {
        Task InsertarVisualizacion(VisualizacionPendiente visualizacion);
        Task<IEnumerable<VisualizacionPendiente>> ObtenerVisualizacionesPorUsuario(Guid idUsuario);
        Task EliminarVisualizacion(Guid idPendiente);
        Task ActualizarPrioridad(Guid idPendiente, int nuevaPrioridad);
    }
}
