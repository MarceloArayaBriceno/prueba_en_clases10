using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Servicios.Peliculas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerieController : ControllerBase, ISerieController
    {
        private ISerieFlujo _SerieFlujo;
        private ILogger<SerieController> _logger;

        public SerieController(ISerieFlujo SerieFlujo, ILogger<SerieController> logger)
        {
            _SerieFlujo = SerieFlujo;
            _logger = logger;
        }
        #region "Operaciones"
        [HttpGet("detalle/{Id}")]
        public async Task<IActionResult> ObtenerDetalle([FromRoute] int Id)
        {
            var resultado = await _SerieFlujo.ObtenerDetalle(Id);
            return Ok(resultado);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] int Id)
        {
            var resultado = await _SerieFlujo.Obtener(Id);
            return Ok(resultado);
        }

        [HttpGet("generos")]
        public async Task<IActionResult> ObtenerGeneros()
        {
            var resultado = await _SerieFlujo.ObtenerGeneros();
            return Ok(resultado);
        }
        #endregion
        #region Helpers
        #endregion
    }
}
