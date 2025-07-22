using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Series
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        [BindProperty(SupportsGet = true)]  
        public int? Genero { get; set; }
        public List<Serie> Series { get; set; } = default!;
        public List<Genero> Generos { get; set; } = default!;

        public async Task OnGetAsync()
        {
            string urlGeneros = _configuracion.ObtenerMetodo("ApiEndPointsSerie", "ObtenerGeneros");
            var client = new HttpClient();
            var respuestaGeneros = await client.GetAsync(urlGeneros);
            respuestaGeneros.EnsureSuccessStatusCode();

            var resultadoGeneros = await respuestaGeneros.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Generos = JsonSerializer.Deserialize<List<Genero>>(resultadoGeneros, opciones)!;

            int idGenero = Genero ?? Generos.FirstOrDefault()?.Id ?? 0;

            string endpointSeries = _configuracion.ObtenerMetodo("ApiEndPointsSerie", "ObtenerSerie");
            string urlSeries = string.Format(endpointSeries, idGenero);
            var respuestaSeries = await client.GetAsync(urlSeries);
            respuestaSeries.EnsureSuccessStatusCode();

            var resultadoSeries = await respuestaSeries.Content.ReadAsStringAsync();
            Series = JsonSerializer.Deserialize<List<Serie>>(resultadoSeries, opciones)!;
        }
    }
}
