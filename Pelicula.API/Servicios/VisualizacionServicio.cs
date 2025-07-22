using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios
{
    public class VisualizacionServicio : IVisualizacionServicio
    {
        private readonly IVisualizacionDA _visualizacionDA;

        public VisualizacionServicio(IVisualizacionDA visualizacionDA)
        {
            _visualizacionDA = visualizacionDA;
        }

        public Task InsertarVisualizacion(VisualizacionPendiente visualizacion)
        {
            return _visualizacionDA.InsertarVisualizacion(visualizacion);
        }

        public Task<IEnumerable<VisualizacionPendiente>> ObtenerVisualizacionesPorUsuario(Guid idUsuario)
        {
            return _visualizacionDA.ObtenerVisualizacionesPorUsuario(idUsuario);
        }

        public Task EliminarVisualizacion(Guid idPendiente)
        {
            return _visualizacionDA.EliminarVisualizacion(idPendiente);
        }

        public Task ActualizarPrioridad(Guid idPendiente, int nuevaPrioridad)
        {
            return _visualizacionDA.ActualizarPrioridad(idPendiente, nuevaPrioridad);
        }
    }
}
