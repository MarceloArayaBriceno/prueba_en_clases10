using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Peliculas
{
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public Pelicula pelicula { get; set; } = default!;
        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task<ActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsPelicula", "ObtenerDetallePelicula");
            var client = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await client.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            pelicula = JsonSerializer.Deserialize<Pelicula>(resultado, opciones);
            return Page();
        }
    }
}
