using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase, IPeliculaController
    {
        private IPeliculaFlujo _PeliculaFlujo;
        private ILogger<PeliculaController> _logger;

        public PeliculaController(IPeliculaFlujo PeliculaFlujo, ILogger<PeliculaController> logger)
        {
            _PeliculaFlujo = PeliculaFlujo;
            _logger = logger;
        }
        #region "Operaciones"
        [HttpGet("detalle/{Id}")]
        public async Task<IActionResult> ObtenerDetalle([FromRoute] int Id)
        {
            var resultado = await _PeliculaFlujo.ObtenerDetalle(Id);
            return Ok(resultado);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] int Id)
        {
            var resultado = await _PeliculaFlujo.Obtener(Id);
            return Ok(resultado);
        }
        [HttpGet("generos")]
        public async Task<IActionResult> ObtenerGeneros()
        {
            var resultado = await _PeliculaFlujo.ObtenerGeneros();
            return Ok(resultado);
        }
        #endregion
        #region Helpers
        #endregion
    }
}
