using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Peliculas
{
    public class GenerosModel : PageModel
    {

        private readonly IConfiguracion _configuracion;

        public GenerosModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public List<Genero> Generos { get; set; } = default!;
        public async Task OnGet()
        {
            string urlGeneros = _configuracion.ObtenerMetodo("ApiEndPointsPelicula", "ObtenerGeneros");
            var client = new HttpClient();
            var respuestaGeneros = await client.GetAsync(urlGeneros);
            respuestaGeneros.EnsureSuccessStatusCode();

            var resultadoGeneros = await respuestaGeneros.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Generos = JsonSerializer.Deserialize<List<Genero>>(resultadoGeneros, opciones)!;
        }
    }
}
