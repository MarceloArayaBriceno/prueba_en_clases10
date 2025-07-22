using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Web.Pages.Peliculas
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

        public List<Pelicula> Peliculas { get; set; } = default!;
        public List<Genero> Generos { get; set; } = default!;

        public async Task OnGetAsync()
        {
            
            await ObtenerGeneros();

            
            await ObtenerPeliculas();
        }

        private async Task ObtenerGeneros()
        {
            string urlGeneros = _configuracion.ObtenerMetodo("ApiEndPointsPelicula", "ObtenerGeneros");

            using var client = new HttpClient();
            var respuestaGeneros = await client.GetAsync(urlGeneros);
            respuestaGeneros.EnsureSuccessStatusCode();
            var resultadoGeneros = await respuestaGeneros.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Generos = JsonSerializer.Deserialize<List<Genero>>(resultadoGeneros, opciones) ?? new List<Genero>();
        }

        private async Task ObtenerPeliculas()
        {
            string endpointPeliculas = _configuracion.ObtenerMetodo("ApiEndPointsPelicula", "ObtenerPelicula");

            // Si no hay género seleccionado, usar el primer género disponible o 0 para "todos"
            int generoId = Genero ?? 0;

            string urlPeliculas = string.Format(endpointPeliculas, generoId);

            using var client = new HttpClient();
            var respuestaPeliculas = await client.GetAsync(urlPeliculas);
            respuestaPeliculas.EnsureSuccessStatusCode();
            var resultadoPeliculas = await respuestaPeliculas.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Peliculas = JsonSerializer.Deserialize<List<Pelicula>>(resultadoPeliculas, opciones) ?? new List<Pelicula>();
        }
    }
}


