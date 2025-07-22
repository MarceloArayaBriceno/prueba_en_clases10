using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Series
{
    public class GenerosModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public GenerosModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public List<Genero> Generos { get; set; } = default;
        public async Task OnGet()
        {
            string url = _configuracion.ObtenerMetodo("ApiEndPointsSerie", "ObtenerGeneros");
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Generos = JsonSerializer.Deserialize<List<Genero>>(json, opciones)!;
        }
    }
}
