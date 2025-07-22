using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        // Lista de películas filtradas por género
        public List<Pelicula> Peliculas { get; set; } = default!;

        public async Task OnGetAsync()
        {
            int idGenero = 28; // Género predeterminado

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPointsPelicula", "ObtenerPelicula");
            string url = string.Format(endpoint, idGenero);

            var client = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, url);
            var respuesta = await client.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Peliculas = JsonSerializer.Deserialize<List<Pelicula>>(resultado, opciones)!;
        }
    }
}

