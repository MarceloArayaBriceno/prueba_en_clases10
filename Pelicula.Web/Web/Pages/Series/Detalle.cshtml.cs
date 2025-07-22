using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Series
{
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public Serie serie { get; set; } = default;
        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<ActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsSerie", "ObtenerDetalleSerie");
            var client = new HttpClient();
            var respuesta = await client.GetAsync(string.Format(endpoint, id));
            respuesta.EnsureSuccessStatusCode();

            var json = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            serie = JsonSerializer.Deserialize<Serie>(json, opciones)!;
            return Page();
        }
    }
}
