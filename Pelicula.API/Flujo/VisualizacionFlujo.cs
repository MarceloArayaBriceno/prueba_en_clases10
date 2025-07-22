using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Interfaces.Flujo;

namespace Flujo
{
    public class VisualizacionFlujo : IVisualizacionFlujo
    {
        private readonly IVisualizacionReglas _reglas;

        public VisualizacionFlujo(IVisualizacionReglas reglas)
        {
            _reglas = reglas;
        }

        public async Task InsertarVisualizacion(VisualizacionPendiente visualizacion)
        {
            await _reglas.InsertarVisualizacion(visualizacion);
        }

        public async Task<IEnumerable<object>> ObtenerVisualizacionesPorUsuario(Guid idUsuario)
        {
            return await _reglas.ObtenerVisualizacionesPorUsuario(idUsuario);
        }

        public async Task EliminarVisualizacion(Guid idPendiente)
        {
            await _reglas.EliminarVisualizacion(idPendiente);
        }

        public async Task ActualizarPrioridad(Guid idPendiente, int nuevaPrioridad)
        {
            await _reglas.ActualizarPrioridad(idPendiente, nuevaPrioridad);
        }
    }
}
