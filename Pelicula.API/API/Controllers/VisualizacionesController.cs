using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisualizacionesController : ControllerBase
    {
        private readonly IVisualizacionFlujo _flujo;

        public VisualizacionesController(IVisualizacionFlujo flujo)
        {
            _flujo = flujo;
        }

        [HttpPost]
        public async Task<IActionResult> InsertarVisualizacion([FromBody] VisualizacionPendiente visualizacion)
        {
            await _flujo.InsertarVisualizacion(visualizacion);
            return Ok("Visualización pendiente registrada correctamente.");
        }

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> ObtenerPorUsuario(Guid idUsuario)
        {
            var resultado = await _flujo.ObtenerVisualizacionesPorUsuario(idUsuario);
            return Ok(resultado);
        }

        [HttpDelete("{idPendiente}")]
        public async Task<IActionResult> Eliminar(Guid idPendiente)
        {
            await _flujo.EliminarVisualizacion(idPendiente);
            return Ok("Visualización eliminada correctamente.");
        }


        [HttpPut("{idPendiente}/prioridad/{nuevaPrioridad}")]
        public async Task<IActionResult> ActualizarPrioridad(Guid idPendiente, int nuevaPrioridad)
        {
            
            if (nuevaPrioridad < 1 || nuevaPrioridad > 3)
                return BadRequest("La prioridad debe estar entre 1 y 3.");

            try
            {
                await _flujo.ActualizarPrioridad(idPendiente, nuevaPrioridad);
                return Ok("Prioridad actualizada correctamente.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                
                return StatusCode(500, "Error al actualizar la prioridad en la base de datos.");
            }
        }

    }
}
