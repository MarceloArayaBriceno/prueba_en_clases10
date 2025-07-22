using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FavoritoController : ControllerBase, IFavoritoController
    {
        private readonly IFavoritoFlujo _favoritoFlujo;
        private readonly ILogger<FavoritoController> _logger;

        public FavoritoController(IFavoritoFlujo favoritoFlujo, ILogger<FavoritoController> logger)
        {
            _favoritoFlujo = favoritoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] FavoritoRequest favorito)
        {
            var resultado = await _favoritoFlujo.Agregar(favorito);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

       [HttpPut("{Id}")] //ver si esta
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] FavoritoRequest favorito)
        {
            if (!await VerificarFavoritoExiste(Id))
                return NotFound("El favorito no existe.");

            var resultado = await _favoritoFlujo.Editar(Id, favorito);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            if (!await VerificarFavoritoExiste(Id))
                return NotFound("El favorito no existe.");

            await _favoritoFlujo.Eliminar(Id);
            return NoContent();
        }
        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> ObtenerPorUsuario([FromRoute] String idUsuario)
        {
            var resultado = await _favoritoFlujo.ObtenerPorUsuario(idUsuario);
            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _favoritoFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var resultado = await _favoritoFlujo.Obtener(Id);
            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        private async Task<bool> VerificarFavoritoExiste(Guid Id)
        {
            var favorito = await _favoritoFlujo.Obtener(Id);
            return favorito != null;
        }
    }
}